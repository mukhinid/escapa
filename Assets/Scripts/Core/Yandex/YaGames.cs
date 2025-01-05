using System;
using Logzep.YandexSDK;
using Logzep.YandexSDK.Advertising;
using UnityEngine;

namespace Escapa.Core.Yandex
{
    public sealed class YaGames : MonoBehaviour
    {
        private float _adTime = -100;

        public event EventHandler AdStarted;
        public event EventHandler AdFinished;

        public void ShowAd(Action callback)
        {
            if (CanShowAd())
            {
                AdStarted?.Invoke(this, null);
                YandexSDK.Ads.ShowAdv((result) => callback?.Invoke());
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

        private void Start()
        {
            YandexSDK.OnYSDKInit.AddListener(() =>
            {
                YandexSDK.Ads.OnAdvertShown.AddListener((result) =>
                {
                    if (result != AdvResult.Open)
                    {
                        AdFinished?.Invoke(this, null);
                    }
                });
                ShowAd(null);
            });
            YandexSDK.Init();
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
    }
}
