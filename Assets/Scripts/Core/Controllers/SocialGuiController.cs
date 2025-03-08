using Escapa.Utility;
using UnityEngine;
using Escapa.Social;
using TMPro;
using Escapa.Core.Managers;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        [SerializeField]
        private GameObject _invite;
        [SerializeField]
        private GameObject _loginButton;

        private void Start()
        {
            var auth = SocialManager.IsAuthorized();
            if (auth)
            {
                _invite.SetActive(false);
                _loginButton.SetActive(false);
            }
            else
            {
                var mesh = _loginButton.GetComponentInChildren<TextMeshProUGUI>();
                mesh.color = StyleManager.Colors.Background;
            }
        }

        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void Authorize()
        {
            SocialManager.Authorize(result =>
            {
                _invite.SetActive(!result);
                _loginButton.SetActive(!result);
            });
        }
    }
}
