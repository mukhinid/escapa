using Escapa.Core;
using Escapa.Core.Managers;
using Escapa.Utility;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Escapa.Core.InputSystem_Actions;

namespace Escapa.Game
{
    public sealed class GameController : MonoBehaviour, ICoreActions
    {
        public event Action GameStarted;

        [SerializeField]
        private Player player;

        private InputSystem_Actions _actions;

        private void OnEnable()
        {
            player.Died += OnPlayerDie;
            player.Pressed += OnPlayerPressed;

            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.Core.SetCallbacks(this);
            }
            _actions.Core.Enable();
        }

        private void OnDisable()
        {
            player.Died -= OnPlayerDie;
            player.Pressed -= OnPlayerPressed;

            _actions.Core.Disable();
        }

        private void OnPlayerDie()
        {
            player.Died -= OnPlayerDie;
            ScoreManager.StopCount();

            var time = (int)(ScoreManager.LastTime * 1000);
            SocialManager.SendScore(time);

            SceneManager.LoadScene((int)GameScenes.End);
        }

        private void OnPlayerPressed()
        {
            player.Pressed -= OnPlayerPressed;

            ScoreManager.StartCount();
            GameStarted?.Invoke();
        }

        public void OnGoBack(InputAction.CallbackContext context)
        {
            if (context.action.WasReleasedThisFrame())
            {
                ScoreManager.StopCount();
            }
        }
    }
}
