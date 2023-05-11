using MainNetworkScripts;
using Zenject;
using Menu;
using Menu.Buttons;
using Sounds;

namespace Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UISounds>().FromInstance(FindObjectOfType<UISounds>()).AsSingle();
            Container.Bind<LobbyButtonsListeners>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainMenuNetworkManager>().FromInstance(FindObjectOfType<MainMenuNetworkManager>()).AsSingle();
            Container.Bind<LobbyPlayerUIList>().FromComponentInHierarchy().AsSingle();
        }
    }
}