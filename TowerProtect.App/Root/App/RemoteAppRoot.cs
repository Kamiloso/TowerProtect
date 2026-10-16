using TowerProtect.App.Client;

namespace TowerProtect.App.Root.App;

internal class RemoteAppRoot(IClientManager clientManager) : IAppRoot
{
    public void Update(double deltaTime)
    {
        clientManager.Update(deltaTime);
    }
}
