using Escapa.Utility;

namespace Escapa.Core.Controllers
{
    public class InfoGuiController : GuiControllerBase
    {
        public void GoToMenu() => LoadScene(GameScenes.Menu);
    }
}
