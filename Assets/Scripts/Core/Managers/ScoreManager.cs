using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class ScoreManager
    {
        private const string PlayerPrefKey = "Score{0}";

        public static float CurrentTime => startTime > float.Epsilon ? Time.realtimeSinceStartup - startTime : 0f;

        public static float LastTime { get; private set; }

        public static bool IsHighScore { get; private set; }

        public static void StartCount()
        {
            IsHighScore = false;
            startTime = Time.realtimeSinceStartup;
        }

        public static void StopCount()
        {
            LastTime = CurrentTime;
            startTime = 0f;

            if (LastTime > CurrentHigh)
            {
                IsHighScore = true;
                CurrentHigh = LastTime;
            }
        }

        public static float CurrentHigh
        {
            get
            {
                var key = string.Format(PlayerPrefKey, (int)DifficultyManager.Current.difficulty);
                return PlayerPrefs.GetFloat(key, 0f);
            }
            private set
            {
                var key = string.Format(PlayerPrefKey, (int)DifficultyManager.Current.difficulty);
                PlayerPrefs.SetFloat(key, value);
            }
        }

        private static float startTime;
    }
}
