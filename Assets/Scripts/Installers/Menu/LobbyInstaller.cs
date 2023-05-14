using MainNetworkScripts;
using Zenject;
using Menu;
using Menu.Buttons;
using Menu.GameUI.MenuFrames;
using Sounds;

namespace Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UISounds>().FromInstance(FindObjectOfType<UISounds>()).AsSingle();
            Container.Bind<Frames>().FromInstance(FindObjectOfType<Frames>()).AsSingle();
            Container.Bind<LobbyButtonsListeners>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainMenuNetworkManager>().FromInstance(FindObjectOfType<MainMenuNetworkManager>()).AsSingle();
            Container.Bind<LobbyPlayerUIList>().FromComponentInHierarchy().AsSingle();
        }
    }
}