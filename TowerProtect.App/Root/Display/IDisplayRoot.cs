using System;
using System.Collections.Generic;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Root.Display;

public interface IDisplayRoot
{
    IEnumerable<Sprite> DrawNextFrame();
}
