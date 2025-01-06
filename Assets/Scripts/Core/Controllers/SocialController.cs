using Escapa.Core.Managers;
using Logzep.YandexSDK;
using Logzep.YandexSDK.Advertising;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Escapa.Core.Controllers
{
    public sealed class SocialController : MonoBehaviour
    {
        private float _adTime = -100;

        public event EventHandler AdStarted;
        public event EventHandler AdFinished;

        public void Init(Action callback)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                #if YAGAMES_ENABLED
                    InitYandex(callback);
                #endif
            #else
                LanguageManager.Load(GetLang());
                callback?.Invoke();
            #endif
        }

        public void ShowAd(Action callback)
        {
            if (CanShowAd())
            {
                AdStarted?.Invoke(this, null);
                #if UNITY_WEBGL && !UNITY_EDITOR
                    #if YAGAMES_ENABLED
                        ShowAdYandex((result) => callback?.Invoke());
                    #endif
                #else
                    callback?.Invoke();
                #endif
            }
            else
            {
                callback?.Invoke();
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private bool CanShowAd()
        {
            var now = Time.realtimeSinceStartup;

            if (now - _adTime >= 60)
            {
                _adTime = now;
                return true;
            }

            return false;
        }

        private void InitYandex(Action callback)
        {
            YandexSDK.OnYSDKInit.AddListener(() =>
            {
                LanguageManager.Load(GetLang());

                YandexSDK.Ads.OnAdvertShown.AddListener((result) =>
                {
                    if (result != AdvResult.Open)
                    {
                        AdFinished?.Invoke(this, null);
                    }
                });
                ShowAd(callback);
            });
            YandexSDK.Init();
        }

        private void ShowAdYandex(UnityAction<AdvResult> resultCallback)
        {
            YandexSDK.Ads.ShowAdv(resultCallback);
        }

        private string GetLang()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                #if YAGAMES_ENABLED
                    var lang = YandexSDK.Env.Lang.ToLower();
                    if (lang != "fr" &&  lang != "de" && lang != "ru" && lang != "es")
                    {
                        return "en";
                    }
                    return lang;
                #endif
            #endif

            return Application.systemLanguage switch
            {
                SystemLanguage.French => "fr",
                SystemLanguage.German => "de",
                SystemLanguage.Russian => "ru",
                SystemLanguage.Spanish => "es",
                _ => "en",
            };
        }
    }
}
