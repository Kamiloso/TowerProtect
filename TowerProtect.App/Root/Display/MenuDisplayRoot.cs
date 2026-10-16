using System.Collections.Generic;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Root.Display;

internal class MenuDisplayRoot : IDisplayRoot
{
    private readonly List<Sprite> _sprites = [];

    public IEnumerable<Sprite> DrawNextFrame()
    {
        // TODO: Implement menu display rendering logic here

        _sprites.Clear();
        // fill _sprites here

        return _sprites;
    }
}
