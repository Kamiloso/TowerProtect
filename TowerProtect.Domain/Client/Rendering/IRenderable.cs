using System.Collections.Generic;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Client.Rendering;

public interface IRenderable
{
    Vec2 Position { get; }
    double Rotation { get; }
    IEnumerable<Sprite> Sprites { get; }

    IRenderable Move(Vec2 deltaPosition);
    IRenderable Rotate(double deltaRotation);
    IRenderable MoveLocal(Vec2 deltaPositionLocal);
    IRenderable RotateAround(Vec2 pivot, double deltaRotation);
}
