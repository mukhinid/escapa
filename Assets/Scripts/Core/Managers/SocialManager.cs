using Agava.YandexGames;
using System;
using System.Collections;

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        public static event EventHandler PopupShown;
        public static event EventHandler PopupClosed;

        public static bool IsPopupOpen { get; private set; } = false;
        public static bool IsAuthorized => PlayerAccount.IsAuthorized;

        public static void Authorize(Action<bool> callback)
        {
            PlayerAccount.Authorize(
                () => callback?.Invoke(true),
                (msg) => callback?.Invoke(false)
            );
        }

        public static IEnumerator Init(Action callback)
        {
            return YandexGamesSdk.Initialize(() =>
            {
                YandexGamesSdk.GameReady();
                callback?.Invoke();
            });
        }

        public static string GetLanguage()
        {
#if YAGAMES_ENABLED
            return YandexGamesSdk.Environment.i18n.lang;
#else
            return "en";
#endif
        }

        public static void SendScore(int time, int difficulty)
        {
            if (!IsAuthorized)
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
            Leaderboard.SetScore(id, time);
        }

        public static void ShowAd(Action callback)
        {
            InterstitialAd.Show(
                onOpenCallback: () =>
                {
                    IsPopupOpen = true;
                    PopupShown?.Invoke(null, EventArgs.Empty);
                },
                onCloseCallback: (wasShown) =>
                {
                    IsPopupOpen = false;
                    PopupClosed?.Invoke(null, EventArgs.Empty);
                    callback?.Invoke();
                },
                onErrorCallback: (msg) =>
                {
                    IsPopupOpen = false;
                    PopupClosed?.Invoke(null, EventArgs.Empty);
                    callback?.Invoke();
                },
                onOfflineCallback: () =>
                {
                    IsPopupOpen = false;
                    PopupClosed?.Invoke(null, EventArgs.Empty);
                    callback?.Invoke();
                }
            );
        }
    }
}
