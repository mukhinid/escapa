using Escapa.Social;
using Escapa.Utility;

namespace Escapa.Core.Controllers
{
    public class EndGuiController : GuiControllerBase
    {
        public void Restart() =>
            SocialManager.ShowAd(() => LoadScene(GameScenes.Game));

        public void GoToMenu() =>
            SocialManager.ShowAd(() => LoadScene(GameScenes.Menu));
    }
}
