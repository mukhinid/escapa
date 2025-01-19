using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour
    {
        private const string PlayerPrefKey = "IsSoundMuted";

        [SerializeField]
        private SocialController _social;

        private AudioSource _audioSource;

        public bool IsMuted => _audioSource.mute;

        public void ToggleSound()
        {
            _audioSource.mute = !_audioSource.mute;
            PlayerPrefs.SetInt(PlayerPrefKey, _audioSource.mute ? 1 : 0);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
            _audioSource.mute = PlayerPrefs.GetInt(PlayerPrefKey, 0) == 1;
        }

        private void OnEnable()
        {
            _social.AdStarted += OnAdStarted;
            _social.AdFinished += OnAdFinished;
        }

        private void OnDisable()
        {
            _social.AdStarted -= OnAdStarted;
            _social.AdFinished -= OnAdFinished;
        }

        private void OnAdStarted()
        {
            _audioSource.Pause();
        }

        private void OnAdFinished()
        {
            _audioSource.UnPause();
        }
    }
}
