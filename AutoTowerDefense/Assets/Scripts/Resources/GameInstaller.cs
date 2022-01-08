using Core.GameManager;
using UnityEngine;
using Zenject;

namespace Core.Resources
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalGameManager>().To<LocalGameManager>().AsSingle();
        }
    }
}
