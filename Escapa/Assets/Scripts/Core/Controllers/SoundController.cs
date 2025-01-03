using Escapa.Core.Interfaces;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        private const string PlayerPrefKey = "IsSoundMuted";

        private AudioSource audioSource;

        public bool IsMuted { get; private set; }

        public void ToggleSound()
        {
            IsMuted = !IsMuted;
            audioSource.mute = IsMuted;
        }

        private void Awake()
        {
            IsMuted = PlayerPrefs.GetInt(PlayerPrefKey, 0) == 1;

            audioSource = GetComponent<AudioSource>();
            audioSource.mute = IsMuted;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                PlayerPrefs.SetInt(PlayerPrefKey, IsMuted ? 1 : 0);
        }

        private void OnApplicationQuit() => PlayerPrefs.SetInt(PlayerPrefKey, IsMuted ? 1 : 0);
    }
}
