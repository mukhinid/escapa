using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Escapa.Core.InputSystem_Actions;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(ISoundController))]
    public sealed class SystemController : MonoBehaviour, ICoreActions
    {
        private InputSystem_Actions _actions;

        private GameScenes current;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.Core.SetCallbacks(this);
            }
            _actions.Core.Enable();
        }

        private void Start() => SocialManager.Auth(OnAuthenticated);

        // If user wants to quit using Task Manager.
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                DifficultyManager.Save();
                ScoreManager.Save();
            }
        }

        // If user wants to quit the usual way.
        private void OnApplicationQuit()
        {
            DifficultyManager.Save();
            ScoreManager.Save();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _actions.Core.Disable();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => current = (GameScenes)scene.buildIndex;

        private void OnAuthenticated() => SceneManager.LoadScene((int)GameScenes.Menu, LoadSceneMode.Single);

        public void OnGoBack(InputAction.CallbackContext context)
        {
            if (context.action.WasReleasedThisFrame())
            {
                if (current == GameScenes.Menu)
                    Application.Quit();
                else
                    SceneManager.LoadScene((int)GameScenes.Menu, LoadSceneMode.Single);
            }
        }
    }
}
