using MainNetworkScripts;
using Menu.Buttons;
using Menu.GameUI;
using Menu.GameUI.MenuFrames;
using Sounds;
using Zenject;

namespace Installers.Menu
{
    public class NetworkManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameUI>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Frames>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuButtons>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UISounds>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainMenuNetworkManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}