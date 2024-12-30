using Escapa.Utility;
#if UNITY_WEBGL && !UNITY_EDITOR
using Logzep.YandexSDK;
#endif
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static readonly string[] strings = Load();
        public static string GetString(LanguageTokens token) => strings[(int)token];

        private static string[] Load()
        {
            var lang = GetLang();

            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}/{lang}").text;
            return JsonUtility.FromJson<Language>(json).Strings;
        }

        private static string GetLang()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var lang = YandexSDK.Env.Lang.ToLower();
            if (lang != "fr" &&  lang != "de" && lang != "ru" && lang != "es")
            {
                return "en";
            }
            return lang;
#else
            return Application.systemLanguage switch
            {
                SystemLanguage.French => "fr",
                SystemLanguage.German => "de",
                SystemLanguage.Russian => "ru",
                SystemLanguage.Spanish => "es",
                _ => "en",
            };
#endif
        }
    }
}
