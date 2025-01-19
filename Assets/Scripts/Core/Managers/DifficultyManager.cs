using System;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class DifficultyManager
    {
        private const string PlayerPrefKey = "Difficulty";

        private static readonly Level[] levels = Load();
        private static int currentDifficulty = PlayerPrefs.GetInt(PlayerPrefKey, 0);

        public static event Action DifficultyChanged;

        public static Level Current => levels[currentDifficulty];

        public static void Increase()
        {
            currentDifficulty = Current.difficulty == Difficulties.Insane
                ? (int)Difficulties.Easy
                : currentDifficulty + 1;
            PlayerPrefs.SetInt(PlayerPrefKey, currentDifficulty);
            DifficultyChanged?.Invoke();
        }

        public static void Decrease()
        {
            currentDifficulty = Current.difficulty == Difficulties.Easy
                ? (int)Difficulties.Insane
                : currentDifficulty - 1;
            PlayerPrefs.SetInt(PlayerPrefKey, currentDifficulty);
            DifficultyChanged?.Invoke();
        }

        private static Level[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            return JsonUtility.FromJson<Levels>(json).levels;
        }
    }
}
