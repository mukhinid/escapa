using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class EndGuiController : GuiControllerBase
    {
        private SocialController _social;

        private void Awake()
        {
            _social = GameObject.FindWithTag("YaGames").GetComponent<SocialController>();
        }

        public void Restart()
        {
            _social.ShowAd(() => LoadScene(GameScenes.Game));
        }

        public void GoToMenu()
        {
            _social.ShowAd(() => LoadScene(GameScenes.Menu));
        }
    }
}
