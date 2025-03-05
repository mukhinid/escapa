using Escapa.Utility;

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        public static void CompleteAchievement(string achievementGuid)
        {
            if (!SocialStub.localUser.authenticated) return;

            if (!string.IsNullOrWhiteSpace(achievementGuid))
                SocialStub.ReportProgress(achievementGuid, 100d, success => { });
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
    }
}
