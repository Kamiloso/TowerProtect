using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.Domain.Game.Environment;

public class ClientBorders(int range) : ISpriteRenderingAware
{
    private const double SIZE = 1 << 10;
    private int _range = range;

    public void Recreate(int range) => _range = range;

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        double min = _range + 0.5;
        double max = min + SIZE;
        double mid = (min + max) / 2;

        Vec2[] directions = [Vec2.Up, Vec2.Down, Vec2.Right, Vec2.Left];

        var sprites = directions
            .Select(dir => new Sprite("Test", new Rect(dir * mid, SIZE)));

        targetSprites.AddRange(sprites);
    }
}
