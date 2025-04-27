using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static readonly Lazy<string[]> strings = new(Load());
        public static string GetString(LanguageTokens token) => strings.Value[(int)token];

        private static string[] Load()
        {
            string lang;
            #if YAGAMES_ENABLED
                lang = SocialManager.GetLanguage();
                if (lang != "fr" &&  lang != "de" && lang != "ru" && lang != "es")
                {
                    lang = "en";
                }
            #else
                lang = Application.systemLanguage switch
                {
                    SystemLanguage.French => "fr",
                    SystemLanguage.German => "de",
                    SystemLanguage.Russian => "ru",
                    SystemLanguage.Spanish => "es",
                    _ => "en",
                };
            #endif

            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}/{lang}").text;
            return JsonUtility.FromJson<Language>(json).Strings;
        }
    }
}
