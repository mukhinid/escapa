using System;
using Escapa.Social.YaGames;
using UnityEngine;

namespace Escapa.Social
{
    public static class SocialManager
    {
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
            return Sdk.Environment.i18n.lang.ToLower();
        }

        public static void ShowAd(Action<AdvertShownResult> resultCallback)
        {
            #if YAGAMES_ENABLED
                Sdk.ShowFullScreenAdv(resultCallback);
            #else
                resultCallback?.Invoke(AdvertShownResult.Close);
            #endif
        }
    }
}
