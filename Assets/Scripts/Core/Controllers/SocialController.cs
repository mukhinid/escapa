using System;
using UnityEngine;
using SM = Escapa.Social.SocialManager;

namespace Escapa.Core.Controllers
{
    public sealed class SocialController : MonoBehaviour
    {
        private float _adTime = -100;

        public event Action AdStarted;
        public event Action AdFinished;

        public void ShowAd(Action callback)
        {
            if (CanShowAd())
            {
                AdStarted?.Invoke();
                SM.ShowAd(result =>
                {
                    if (result != Social.AdvertShownResult.Open)
                    {
                        AdFinished?.Invoke();
                    }
                    callback?.Invoke();
                });
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
    }
}
