using TowerProtect.App.Backend;
using TowerProtect.App.Client;

namespace TowerProtect.App.Root.App;

internal class LocalAppRoot(
    IBackendManager backendManager,
    IClientManager clientManager) : IAppRoot
{
    public void Start()
    {
        backendManager.Start();
        clientManager.Start();
    }

    public void Update(double deltaTime)
    {
        backendManager.Update(deltaTime);
        clientManager.Update(deltaTime);
    }
}
