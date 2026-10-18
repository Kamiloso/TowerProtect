using TowerProtect.App.Backend;
using TowerProtect.App.Client;
using TowerProtect.App.Networking.Connectors;

namespace TowerProtect.App.Root.App;

internal class LocalAppRoot(
    ILocalConnector localConnector,
    IBackendManager backendManager,
    IClientManager clientManager) : IAppRoot
{
    public void Start()
    {
        localConnector.Start();
        backendManager.Start();
        clientManager.Start();
    }

    public void Update(double deltaTime)
    {
        backendManager.Update(deltaTime);
        clientManager.Update(deltaTime);
    }
}
