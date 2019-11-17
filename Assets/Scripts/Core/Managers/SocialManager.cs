﻿using Escapa.Utility;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        public static void CompleteAchievement(string achievementGuid)
        {
            if (!Social.localUser.authenticated) return;

            if (!string.IsNullOrWhiteSpace(achievementGuid))
                Social.ReportProgress(achievementGuid, 100d, success => { });
        }

        public static void SendScore(Difficulties difficulty, long time)
        {
            if (!Social.localUser.authenticated) return;

            var leaderboardGuid = string.Empty;

            switch (difficulty)
            {
                case Difficulties.Easy:   leaderboardGuid = GooglePlayIds.leaderboard_easy;        break;
                case Difficulties.Medium: leaderboardGuid = GooglePlayIds.leaderboard_medium;      break;
                case Difficulties.Hard:   leaderboardGuid = GooglePlayIds.leaderboard_hard;        break;
                case Difficulties.Insane: leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            Social.ReportScore(time, leaderboardGuid, success => { });
        }

        public static void Auth(System.Action callback)
        {
            // Check if GPGS is installed
            if (GooglePlayGames.OurUtils.PlatformUtils.Supported)
            {
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.Activate();

                if (Social.localUser.authenticated) return;

                Social.localUser.Authenticate(success =>
                {
                    if (success)
                        ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                });
            }

            callback?.Invoke();
        }

        public static void ShowAchievements()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowAchievementsUI();
        }

        public static void ShowLeaderboards()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowLeaderboardUI();
        }
    }
}