using Escapa.Core;
using Escapa.Core.Managers;
using Escapa.Social;
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

        private float? idleTime;
        private float? movingTime;

        private void OnEnable()
        {
            player.Died += OnPlayerDie;
            player.Moved += OnPlayerMoved;
            player.Pressed += OnPlayerPressed;
            player.Stopped += OnPlayerStopped;

            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.Core.SetCallbacks(this);
            }
            _actions.Core.Enable();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                OnPlayerDie();
            }
        }

        private void OnDisable()
        {
            player.Died -= OnPlayerDie;
            player.Moved -= OnPlayerMoved;
            player.Pressed -= OnPlayerPressed;
            player.Stopped -= OnPlayerStopped;

            _actions.Core.Disable();
        }

        private void OnPlayerDie()
        {
            player.Died -= OnPlayerDie;
            ScoreManager.StopCount();

            var time = (long)(ScoreManager.LastTime * 1000);
            var level = (int)DifficultyManager.Current.difficulty;
            SocialManager.SendScore(time, level);

            SceneManager.LoadScene((int)GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!movingTime.HasValue)
            {
                movingTime = ScoreManager.CurrentTime;
                idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            player.Pressed -= OnPlayerPressed;

            ScoreManager.StartCount();
            GameStarted?.Invoke();
        }

        private void OnPlayerStopped()
        {
            if (!idleTime.HasValue)
            {
                idleTime = ScoreManager.CurrentTime;
                movingTime = null;
            }
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
