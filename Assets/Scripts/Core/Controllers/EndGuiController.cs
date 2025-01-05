using Escapa.Core.Yandex;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class EndGuiController : GuiControllerBase
    {
        private YaGames _yaGames;

        private void Awake()
        {
            _yaGames = GameObject.FindWithTag("YaGames").GetComponent<YaGames>();
        }

        public void Restart()
        {
            _yaGames.ShowAd(() => LoadScene(GameScenes.Game));
        }

        public void GoToMenu()
        {
            _yaGames.ShowAd(() => LoadScene(GameScenes.Menu));
        }
    }
}
