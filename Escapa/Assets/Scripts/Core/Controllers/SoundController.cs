using Escapa.Core.Interfaces;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        private const string PlayerPrefKey = "IsSoundMuted";

        private AudioSource audioSource;

        public bool IsMuted => audioSource.mute;

        public void ToggleSound()
        {
            audioSource.mute = !audioSource.mute;
            PlayerPrefs.SetInt(PlayerPrefKey, audioSource.mute ? 1 : 0);
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = PlayerPrefs.GetInt(PlayerPrefKey, 0) == 1;
        }
    }
}
