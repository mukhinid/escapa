using Escapa.Assets.Scripts.Utility;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class ScoreManager
    {
        private const string PlayerPrefKey = "Score{0}";

        public static float CurrentTime => startTime > float.Epsilon ? Time.realtimeSinceStartup - startTime : 0f;

        public static float LastTime { get; private set; }

        public static bool IsHighScore { get; private set; }

        /// <summary>
        /// The time after which we give the achievement "Black hawk"
        /// </summary>
        public static float BlackHawkTime => times.BlackHawk;

        /// <summary>
        /// The time after which we give the achievement "Zen"
        /// </summary>
        public static float ZenTime => times.Zen;

        /// <summary>
        /// The time after which we give the achievement "Moves Like Jagger"
        /// </summary>
        public static float JaggerTime => times.Jagger;

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
        private static readonly TimeConfig times = LoadConfig();

        private static TimeConfig LoadConfig()
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Achievements}").text;
            return JsonUtility.FromJson<AchievementsConfig>(json).Time;
        }
    }
}
