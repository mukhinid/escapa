﻿using Escapa.Controllers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour
    {
        public Difficulties difficulty;

        private float _minSpeed;
        private float _maxSpeed;
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IGameController _gameController;
        private IStyleController _styleController;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            var go = GameObject.FindWithTag(Tags.GameController);
            _gameController = go.GetComponent<IGameController>();
            _styleController = go.GetComponent<IStyleController>();
        }
        
        private void OnEnable()
        {
            _gameController.GameInitialized += OnGameInitialized;
            _gameController.GameStarted += OnGameStarted;
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void OnDisable()
        {
            _gameController.GameInitialized -= OnGameInitialized;
            _gameController.GameStarted -= OnGameStarted;
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnGameInitialized(GameEventArgs e)
        {
            _minSpeed = e.Level.minSpeed;
            _maxSpeed = e.Level.maxSpeed;
            
            if (e.Level.difficulty < difficulty)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnGameStarted(GameEventArgs e)
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
        
        private void OnStyleChanged(StyleEventArgs e)
        {
            _spriteRenderer.color = e.Theme.Enemy;
        }
    }
}
