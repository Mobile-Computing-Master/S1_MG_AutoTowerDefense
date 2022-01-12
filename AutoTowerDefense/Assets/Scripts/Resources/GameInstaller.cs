using Core.GameManager;
using Core.Map;
using Core.UI;
using Zenject;

namespace Resources
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalGameManager>().To<LocalGameManager>().AsSingle();
            Container.Bind<IUiController>().To<UiController>().AsSingle();
            Container.Bind<IMapManager>().To<MapManager>().AsSingle();
        }
    }
}
