using UnityEngine;

namespace Escapa.Utility
{
    public static class SocialStub
    {
        public class LocalUser
        {
            public bool authenticated => false;
        }

        public static LocalUser localUser = new();

        public static void ShowLeaderboardUI()
        {
            Debug.Log("Showing leaderboard");
        }

        public static void ShowAchievementsUI()
        {
            Debug.Log("Showing achievements");
        }

        public static void ReportScore(long score, string board, System.Action<bool> callback)
        {
            Debug.Log($"Reported {score} to {board}");
            callback?.Invoke(true);
        }

        public static void ReportProgress(string achievementID, double progress, System.Action<bool> callback)
        {
            Debug.Log($"Reported {progress} to {achievementID}");
            callback?.Invoke(true);
        }
    }
}
