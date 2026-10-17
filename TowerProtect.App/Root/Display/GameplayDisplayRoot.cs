using System.Collections.Generic;
using TowerProtect.App.Client;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Root.Display;

internal class GameplayDisplayRoot(
    IClientState clientState,
    ICameraView cameraView) : IDisplayRoot
{
    private readonly List<Sprite> _targetSprites = [];

    public IEnumerable<Sprite> DrawNextFrame()
    {
        _targetSprites.Clear();
        clientState.RenderSprites(_targetSprites, cameraView);

        return _targetSprites;
    }
}
