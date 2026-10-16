using System.Collections.Generic;
using TowerProtect.App.Client;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Root.Display;

internal class GameplayDisplayRoot(
    IClientState clientState,
    ICameraView cameraView) : IDisplayRoot
{
    private readonly List<Sprite> _sprites = [];

    public IEnumerable<Sprite> DrawNextFrame()
    {
        _sprites.Clear();

        clientState.Tilemap.RenderSprites(_sprites, cameraView);
        clientState.Borders.RenderSprites(_sprites, cameraView);

        return _sprites;
    }
}
