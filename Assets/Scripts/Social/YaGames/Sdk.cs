using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Escapa.Social.YaGames
{
    internal static class Sdk
    {
        private static Action _onInit;
        private static Action<AdvertShownResult> _onAdvertShown;
        private static Action<bool> _onAuthorized;

        public static Environment Environment
        {
            get
            {
                var json = GetEnvironmentJson();
                return JsonUtility.FromJson<Environment>(json);
            }
        }

        public static void Init(Action callback)
        {
            _onInit = callback;
            InternalInit(OnInitCallback);
        }

        public static void ShowFullScreenAdv(Action<AdvertShownResult> callback)
        {
            _onAdvertShown = callback;
            ShowFullScreenAdv(OnAdvertShownCallback);
        }

        public static void Authorize(Action<bool> callback)
        {
            _onAuthorized = callback;
            InternalAuthorize(OnAuthorizedCallback);
        }

        [DllImport("__Internal", EntryPoint = "Init")]
        private static extern void InternalInit(Action callback);

        [DllImport("__Internal")]
        private static extern string GetEnvironmentJson();

        [DllImport("__Internal")]
        private static extern void ShowFullScreenAdv(Action<int> callback);

        [DllImport("__Internal")]
        public static extern bool IsAuthorized();

        [DllImport("__Internal")]
        public static extern void SendScore(string lbName, long score);

        [DllImport("__Internal", EntryPoint = "Authorize")]
        private static extern void InternalAuthorize(Action<int> callback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnInitCallback() => _onInit?.Invoke();

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnAdvertShownCallback(int result) => _onAdvertShown?.Invoke((AdvertShownResult)result);

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnAuthorizedCallback(int result) => _onAuthorized?.Invoke(result == 1);
    }
}
