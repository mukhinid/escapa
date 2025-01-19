using Escapa.Core.Controllers;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Scale))]
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Difficulties difficulty;
        [SerializeField]
        private GameController _gameController;

        private float minSpeed;
        private float maxSpeed;

        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable() => _gameController.GameStarted += OnGameStarted;

        private void Start()
        {
            if (DifficultyManager.Current.difficulty < difficulty)
                gameObject.SetActive(false);
            else
            {
                minSpeed = DifficultyManager.Current.minSpeed;
                maxSpeed = DifficultyManager.Current.maxSpeed;
            }
        }

        private void OnDisable() => _gameController.GameStarted -= OnGameStarted;

        private void OnGameStarted()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
    }
}
