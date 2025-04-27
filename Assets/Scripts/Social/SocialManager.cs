using System;
using Escapa.Social.YaGames;
using UnityEngine;

namespace Escapa.Social
{
    public static class SocialManager
    {
        private static float _adTime = -100;

        public static event Action PopupShowed;
        public static event Action PopupClosed;

        public static bool IsPopupOpen { get; private set; }

        public static void Init(Action callback)
        {
            #if YAGAMES_ENABLED
                Sdk.Init(callback);
            #else
                callback?.Invoke();
            #endif
        }

        public static string GetLanguage()
        {
            #if YAGAMES_ENABLED
                return Sdk.Environment.i18n.lang.ToLower();
            #else
                return "en";
            #endif
        }

        public static void ShowAd(Action callback)
        {
            if (CanShowAd())
            {
                #if YAGAMES_ENABLED
                    Sdk.ShowFullScreenAdv(result =>
                    {
                        switch (result)
                        {
                            case AdvertShownResult.Open:
                            {
                                PopupShowed?.Invoke();
                                IsPopupOpen = true;
                                return;
                            }
                            default:
                                {
                                IsPopupOpen = false;
                                PopupClosed?.Invoke();
                                callback?.Invoke();
                                return;
                            }
                        }
                    });
                #else
                    callback?.Invoke();
                #endif
            }
            else
            {
                callback?.Invoke();
            }
        }

        public static void Authorize(Action<bool> callback)
        {
            #if YAGAMES_ENABLED
                PopupShowed?.Invoke();
                IsPopupOpen = true;
                Sdk.Authorize((res) =>
                {
                    IsPopupOpen = false;
                    PopupClosed?.Invoke();
                    callback?.Invoke(res);
                });
            #else
                callback?.Invoke(false);
            #endif
        }

        public static bool IsAuthorized()
        {
            #if YAGAMES_ENABLED
                return Sdk.IsAuthorized();
            #else
                return false;
            #endif
        }

        public static void SendScore(long time, int difficulty)
        {
            if (!IsAuthorized())
            {
                return;
            }

            var id = difficulty switch
            {
                1 => "medium1",
                2 => "hard1",
                3 => "insane1",
                _ => "easy1",
            };
            Sdk.SendScore(id, time);
        }

        private static bool CanShowAd()
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
