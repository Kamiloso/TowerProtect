using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Rendering.Sprites;

namespace TowerProtect.App.Root.Display;

public interface IDisplayRoot
{
    IEnumerable<Sprite> DrawNextFrame();
}
