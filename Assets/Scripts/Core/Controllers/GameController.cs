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

        private void FixedUpdate()
        {
            if (ScoreManager.CurrentTime > ScoreManager.BlackHawkTime && DifficultyManager.Current.difficulty == Difficulties.Insane)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (idleTime.HasValue && ScoreManager.CurrentTime - idleTime.Value > ScoreManager.ZenTime)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (movingTime.HasValue && ScoreManager.CurrentTime - movingTime > ScoreManager.JaggerTime)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_moves_like_jagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_panic_button);

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
            SocialManager.SendScore();

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
