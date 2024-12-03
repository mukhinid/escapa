using Escapa.Utility;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        public void GoToMenu() => LoadScene(GameScenes.Menu);
    }
}
