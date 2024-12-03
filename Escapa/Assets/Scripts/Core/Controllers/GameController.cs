using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Escapa.Core.InputSystem_Actions;

namespace Escapa.Core.Controllers
{
    public sealed class GameController : MonoBehaviour, IGameController, ICoreActions
    {
        public event GameEvent GameStarted;

        private InputSystem_Actions _actions;

        private IPlayer player;

        private float? idleTime;
        private float? movingTime;

        private void Awake() => player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();

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
