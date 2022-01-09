using Core.GameManager;
using Core.UI;
using UnityEngine;
using Zenject;

namespace Core.Resources
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalGameManager>().To<LocalGameManager>().AsSingle();
            Container.Bind<IUiController>().To<UiController>().AsSingle();
        }
    }
}
