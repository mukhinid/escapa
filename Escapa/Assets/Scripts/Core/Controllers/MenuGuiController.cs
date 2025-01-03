using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Core.Controllers
{
    public sealed class MenuGuiController : GuiControllerBase
    {
        [SerializeField]
        private Sprite soundOn;
        [SerializeField]
        private Sprite soundOff;

        private TextMeshProUGUI difficultyButton;
        private SoundController _soundController;
        private Image soundButton;

        public void AddDifficulty()
        {
            DifficultyManager.Increase();
            difficultyButton.SetText(GetString(DifficultyManager.Current.difficulty));
        }

        public void GoToInfo() => LoadScene(GameScenes.Info);

        public void GoToSocial() => LoadScene(GameScenes.Social);

        public void StartGame() => LoadScene(GameScenes.Game);

        public void ToggleSound()
        {
            _soundController.ToggleSound();
            soundButton.overrideSprite = _soundController.IsMuted ? soundOff : soundOn;
        }

        private void Awake()
        {
            difficultyButton = GameObject.FindWithTag(Tags.DifficultyButton).GetComponent<TextMeshProUGUI>();
            _soundController = GameObject.FindWithTag("BackgroundMusic").GetComponent<SoundController>();
            soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start()
        {
            difficultyButton.SetText(GetString(DifficultyManager.Current.difficulty));
            soundButton.overrideSprite = _soundController.IsMuted ? soundOff : soundOn;
        }

        private string GetString(Difficulties difficulty)
        {
            switch (difficulty)
            {
                default:
                case Difficulties.Easy:
                    return LanguageManager.GetString(LanguageTokens.DifficultyEasy);
                case Difficulties.Medium:
                    return LanguageManager.GetString(LanguageTokens.DifficultyMedium);
                case Difficulties.Hard:
                    return LanguageManager.GetString(LanguageTokens.DifficultyHard);
                case Difficulties.Insane:
                    return LanguageManager.GetString(LanguageTokens.DifficultyInsane);
            }
        }
    }
}
