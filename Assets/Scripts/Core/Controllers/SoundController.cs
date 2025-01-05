using Escapa.Core.Interfaces;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour
    {
        private const string PlayerPrefKey = "IsSoundMuted";

        private AudioSource _audioSource;

        public bool IsMuted => _audioSource.mute;

        public void ToggleSound()
        {
            _audioSource.mute = !_audioSource.mute;
            PlayerPrefs.SetInt(PlayerPrefKey, _audioSource.mute ? 1 : 0);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.mute = PlayerPrefs.GetInt(PlayerPrefKey, 0) == 1;
        }

        private void OnApplicationFocus(bool focus)
        {
            _audioSource.mute = !focus;
        }
    }
}
