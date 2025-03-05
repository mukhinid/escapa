using Escapa.Utility;
using UnityEngine;
using Escapa.Social;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        [SerializeField]
        private GameObject[] _forLogin;

        private void Start()
        {
            var auth = SocialManager.IsAuthorized();
            foreach (var go in _forLogin)
            {
                go.SetActive(!auth);
            }
        }

        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void Authorize()
        {
            SocialManager.Authorize(result =>
            {
                foreach (var go in _forLogin)
                {
                    go.SetActive(!result);
                }
            });
        }
    }
}
