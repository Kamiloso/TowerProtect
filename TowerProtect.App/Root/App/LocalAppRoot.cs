using System.Collections.Generic;
using TowerProtect.App.Backend;
using TowerProtect.App.Client;
using TowerProtect.App.Networking.Connectors;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Root.App;

internal class LocalAppRoot(
    ILocalConnector localConnector,
    IBackendOrchestrator backendOrchestrator,
    IClientOrchestrator clientOrchestrator) : IAppRoot
{
    IEnumerable<object> IAwareChain.Children => [
        localConnector,
        backendOrchestrator,
        clientOrchestrator,
    ];
}
