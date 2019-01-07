﻿using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Managers
{
    public static class LanguageManager
    {
        private static Language _language = null;
        public static Language Language
        {
            get
            {
                if (_language == null) LoadLanguage();
                return _language;
            }
        }

        private static void LoadLanguage()
        {
            var lang = "en";
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian: lang = "ru"; break;
            }

            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}/{lang}").text;
            _language = JsonUtility.FromJson<Language>(json);
        }
    }

    [Serializable]
    public class Language
    {
        public string[] Difficulties;
        public string Logo;
        public string NewRecord;
        public string NewHighScore;
        public string Play;
        public string Time;
    }
}