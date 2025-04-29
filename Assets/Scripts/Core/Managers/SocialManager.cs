using Agava.YandexGames;
using Escapa.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public static void GetLeaderboard(Action<string, IList<(string, string, int)>> callback)
        {
            var id = GetLeaderboardId();
            Leaderboard.GetEntries(id, (res) =>
            {
                var entries = new List<(string, string, int)>(5);
                for (var i = 0; i < res.entries.Length; i++)
                {
                    entries.Add(new(res.entries[i].player.profilePicture, res.entries[i].player.publicName, res.entries[i].score));
                }

                var title = GetLanguage() == "ru"
                    ? $"ТОП 5 - {res.leaderboard.title.ru}"
                    : $"TOP 5 - {res.leaderboard.title.en}";
                callback?.Invoke(title, entries);
            }, competingPlayersCount: 0, includeSelf: false);
        }

        public static void DownloadImage(string url, Action<Sprite> callback)
        {
            var image = new RemoteImage(url);
            image.Download((texture) => callback?.Invoke(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero)));
        }

        public static void SendScore(int time)
        {
            if (!IsAuthorized)
            {
                return;
            }

            var id = GetLeaderboardId();
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

        private static string GetLeaderboardId() => DifficultyManager.Current.difficulty switch
        {
            Difficulties.Medium => "medium1",
            Difficulties.Hard => "hard1",
            Difficulties.Insane => "insane1",
            _ => "easy1",
        };
    }
}
