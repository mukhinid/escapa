using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using SM = Escapa.Social.SocialManager;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        [SerializeField]
        private GameObject _achievementsButton;
        [SerializeField]
        private GameObject _lbButton;

        private void Start()
        {
            var auth = SM.IsAuthorized;
            _achievementsButton.SetActive(auth);
            _lbButton.SetActive(auth);
        }

        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void ShowAchievements() => SocialManager.ShowAchievements();

        public void ShowLeaderboards() => SocialManager.ShowLeaderboards();
    }
}
