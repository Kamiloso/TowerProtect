using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Root.App;

internal class MenuAppRoot : IAppRoot
{
    IEnumerable<object> IAwareChain.Children => [];
}
