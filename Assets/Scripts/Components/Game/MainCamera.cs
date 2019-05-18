﻿using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Components.Game
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => _camera.orthographicSize * 2.0f / Screen.height;

        public Vector2 ScreenToWorldPoint(Vector2 point) => _camera.ScreenToWorldPoint(point);
        
        private Camera _camera;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDifficultyChanged() => _camera.backgroundColor = StyleManager.Current.background;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != (int) GameScenes.Preload)
                _camera.backgroundColor = StyleManager.Current.background;
        }
    }
}