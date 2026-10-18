using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Rendering;

namespace TowerProtect.App.Root.Display;

public interface IDisplayRoot
{
    IEnumerable<Sprite> DrawNextFrame();
}
