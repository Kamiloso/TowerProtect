using System.Collections.Generic;
using TowerProtect.App.Client;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Root.App;

internal class RemoteAppRoot(
    IClientOrchestrator clientOrchestrator) : IAppRoot
{
    IEnumerable<object> IAwareChain.Children => [
        clientOrchestrator,
    ];
}
