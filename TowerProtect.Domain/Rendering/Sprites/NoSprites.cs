using System.Collections.Generic;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Rendering.Sprites;

public sealed class NoSprites : IRenderable
{
    public Vec2 Position => Vec2.Zero;
    public double Rotation => 0.0;
    public IEnumerable<Sprite> Sprites => [];

    public static NoSprites Instance { get; } = new();
    private NoSprites() { }

    public IRenderable Move(Vec2 deltaPosition) => this;
    public IRenderable Rotate(double deltaRotation) => this;
    public IRenderable MoveLocal(Vec2 deltaPositionLocal) => this;
    public IRenderable RotateAround(Vec2 pivot, double deltaRotation) => this;
}
