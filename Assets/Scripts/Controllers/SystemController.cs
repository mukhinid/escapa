﻿using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public bool IsSoundEnabled
        {
            get => !_audioSource.mute;
            set => _audioSource.mute = !value;
        }

        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));

            _audioSource = GetComponent<AudioSource>();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Application.quitting += OnApplicationQuit;
        }

        private void Start() => IsSoundEnabled = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundEnabled, 1) == 1;

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            ScoreManager.SaveRecords();
            SocialManager.SaveAchievementsLocal();
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundEnabled, IsSoundEnabled ? 1 : 0);
        }
    }
}
