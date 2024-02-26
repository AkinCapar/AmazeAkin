using Zenject;

namespace Amaze.Installer
{
    public class GameSignalsInstaller :Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<LevelInitializedSignal>().OptionalSubscriber();
        }
    }
}
