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

        public event Action AdStarted;
        public event Action AdFinished;

        public void Init(Action callback)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                #if YAGAMES_ENABLED
                    InitYandex(callback);
                #endif
            #else
                callback?.Invoke();
            #endif
        }

        public void ShowAd(Action callback)
        {
            if (CanShowAd())
            {
                AdStarted?.Invoke();
                #if UNITY_WEBGL && !UNITY_EDITOR
                    #if YAGAMES_ENABLED
                        ShowAdYandex((result) => callback?.Invoke());
                    #endif
                #else
                    AdFinished?.Invoke();
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
                YandexSDK.Ads.OnAdvertShown.AddListener((result) =>
                {
                    if (result != AdvResult.Open)
                    {
                        AdFinished?.Invoke();
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
    }
}
