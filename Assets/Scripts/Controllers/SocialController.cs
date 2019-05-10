﻿using Escapa.Managers;
using Escapa.Utility;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour, ISocialController
    {
        public void CompleteAchievement(Achievements achievement)
        {
            if (!Social.localUser.authenticated && _achievementsFlags[(int)achievement]) return;

            var achievementGuid = string.Empty;

            switch(achievement)
            {
                case Achievements.BlackHawk:
                    achievementGuid = GooglePlayIds.achievement_black_hawk; break;
                case Achievements.MovesLikeJagger:
                    achievementGuid = GooglePlayIds.achievement_moves_like_jagger; break;
                case Achievements.PanicButton:
                    achievementGuid = GooglePlayIds.achievement_panic_button; break;
                case Achievements.Zen:
                    achievementGuid = GooglePlayIds.achievement_zen; break;
            }

            _achievementsFlags[(int)achievement] = true;
            Social.ReportProgress(achievementGuid, 100d, (success) => { });
        }

        public void SendScore()
        {
            if (!Social.localUser.authenticated) return;

            var time = (long)(ScoreManager.LastTime * 1000);
            var leaderboardGuid = string.Empty;

            switch (DifficultyManager.Level)
            {
                case Difficulties.Easy:
                    leaderboardGuid = GooglePlayIds.leaderboard_easy; break;
                case Difficulties.Medium:
                    leaderboardGuid = GooglePlayIds.leaderboard_medium; break;
                case Difficulties.Hard:
                    leaderboardGuid = GooglePlayIds.leaderboard_hard; break;
                case Difficulties.Insane:
                    leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            Social.ReportScore(time, leaderboardGuid, (success) => { });
        }

        public void SaveAchievementsLocal()
        {
            for (var i = 0; i < AchievementsCount; i++)
            {
                var achievementName = string.Empty;

                switch ((Achievements)i)
                {
                    case Achievements.BlackHawk: achievementName = PlayerPrefKeys.BlackHawk; break;
                    case Achievements.MovesLikeJagger: achievementName = PlayerPrefKeys.MovesLikeJagger; break;
                    case Achievements.PanicButton: achievementName = PlayerPrefKeys.PanicButton; break;
                    case Achievements.Zen: achievementName = PlayerPrefKeys.Zen; break;
                }

                PlayerPrefs.SetInt(achievementName, _achievementsFlags[i] ? 1 : 0);
            }
        }

        private const int AchievementsCount = 6;
        private bool[] _achievementsFlags;

        private ISystemController _systemController;

        private void Awake()
        {
            _systemController = GetComponent<ISystemController>();
        }

        private void Start()
        {
            Auth();
        }
        
        private void Auth()
        {
            var config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            Social.localUser.Authenticate(success => 
            {
                if (success)
                {
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                }

                _achievementsFlags = new bool[AchievementsCount];
                for (var i = 0; i < AchievementsCount; i++)
                {
                    var achievementName = string.Empty;

                    switch ((Achievements)i)
                    {
                        case Achievements.BlackHawk: achievementName = PlayerPrefKeys.BlackHawk; break;
                        case Achievements.MovesLikeJagger: achievementName = PlayerPrefKeys.MovesLikeJagger; break;
                        case Achievements.PanicButton: achievementName = PlayerPrefKeys.PanicButton; break;
                        case Achievements.Zen: achievementName = PlayerPrefKeys.Zen; break;
                    }

                    _achievementsFlags[i] = PlayerPrefs.GetInt(achievementName, 0) != 0;
                }

                _systemController.GoToScene(GameScenes.Menu);
            });
        }
    }
}
