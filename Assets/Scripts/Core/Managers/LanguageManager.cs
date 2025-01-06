using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static string[] strings = Array.Empty<string>();
        public static string GetString(LanguageTokens token) => strings[(int)token];

        public static void Load(string lang)
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}/{lang}").text;
            strings = JsonUtility.FromJson<Language>(json).Strings;
        }
    }
}
