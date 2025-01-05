using Escapa.Utility;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using Logzep.YandexSDK;
#endif

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        private static float _adTime = -100;

        public static bool IsAuthenticated => SocialStub.localUser.authenticated;

        public static void CompleteAchievement(string achievementGuid)
        {
            if (!SocialStub.localUser.authenticated) return;

            if (!string.IsNullOrWhiteSpace(achievementGuid))
                SocialStub.ReportProgress(achievementGuid, 100d, success => { });
        }

        public static void SendScore()
        {
            if (!SocialStub.localUser.authenticated) return;

            var time = (long)(ScoreManager.LastTime * 1000);
            var leaderboardGuid = string.Empty;

            switch (DifficultyManager.Current.difficulty)
            {
                case Difficulties.Easy:   leaderboardGuid = GooglePlayIds.leaderboard_easy;        break;
                case Difficulties.Medium: leaderboardGuid = GooglePlayIds.leaderboard_medium;      break;
                case Difficulties.Hard:   leaderboardGuid = GooglePlayIds.leaderboard_hard;        break;
                case Difficulties.Insane: leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            SocialStub.ReportScore(time, leaderboardGuid, success => { });
        }

        public static void Auth(System.Action callback)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexSDK.OnYSDKInit.AddListener(() =>
            {
                ShowAd(() => callback?.Invoke());
            });
            YandexSDK.Init();
#else
            callback?.Invoke();
#endif
        }

        public static void ShowAchievements()
        {
            if (!SocialStub.localUser.authenticated) return;

            SocialStub.ShowAchievementsUI();
        }

        public static void ShowLeaderboards()
        {
            if (!SocialStub.localUser.authenticated) return;

            SocialStub.ShowLeaderboardUI();
        }

        public static void ShowAd(System.Action callback)
        {
            if (CanShowAd())
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                YandexSDK.Ads.ShowAdv((res) => callback?.Invoke());
#endif
            }
        }

        private static bool CanShowAd()
        {
            var now = Time.realtimeSinceStartup;

            if (now - _adTime >= 60)
            {
                _adTime = now;
                return true;
            }

            return false;
        }
    }
}
