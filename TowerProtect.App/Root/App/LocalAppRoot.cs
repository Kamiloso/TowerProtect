using TowerProtect.App.Backend;
using TowerProtect.App.Client;
using TowerProtect.App.Networking.Connectors;

namespace TowerProtect.App.Root.App;

internal class LocalAppRoot(
    ILocalConnector localConnector,
    IClientOrchestrator clientOrchestrator,
    IBackendOrchestrator backendOrchestrator) : IAppRoot
{
    public void Start()
    {
        localConnector.Start();
        clientOrchestrator.Start();
        backendOrchestrator.Start();
    }

    public void Update(double deltaTime)
    {
        clientOrchestrator.Update(deltaTime);
        backendOrchestrator.Update(deltaTime);
    }
}
