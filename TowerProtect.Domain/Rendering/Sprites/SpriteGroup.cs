using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Rendering.Sprites;

public sealed class SpriteGroup(
    Vec2 position,
    IEnumerable<IRenderable> items,
    double rotation = 0.0) : IRenderable
{
    private readonly ImmutableArray<IRenderable> _items = [.. items];

    public Vec2 Position { get; } = position;
    public double Rotation { get; } = rotation;
    public IEnumerable<Sprite> Sprites => _items.SelectMany(i => i.Sprites);

    public SpriteGroup(IEnumerable<IRenderable> items, double rotation = 0.0)
        : this(Vec2.Zero, items, rotation) { }

    public IRenderable Move(Vec2 deltaPosition)
    {
        if (deltaPosition == Vec2.Zero)
            return this;

        return new SpriteGroup(
            Position + deltaPosition,
            _items.Select(i => i.Move(deltaPosition)),
            Rotation
        );
    }

    public IRenderable Rotate(double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        double newRotation = (Rotation + deltaRotation) % (Math.PI * 2);

        return new SpriteGroup(
            Position,
            _items.Select(i => i.RotateAround(Position, deltaRotation)),
            newRotation
        );
    }

    public IRenderable MoveLocal(Vec2 deltaPositionLocal)
    {
        if (deltaPositionLocal == Vec2.Zero)
            return this;

        Vec2 rotatedDelta = deltaPositionLocal.Rotate(Rotation);
        return Move(rotatedDelta);
    }

    public IRenderable RotateAround(Vec2 pivot, double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        Vec2 offset = Position - pivot;
        Vec2 rotatedOffset = offset.Rotate(deltaRotation);
        Vec2 newPosition = pivot + rotatedOffset;
        double newRotation = (Rotation + deltaRotation) % (Math.PI * 2);

        return new SpriteGroup(
            newPosition,
            _items.Select(i => i.RotateAround(pivot, deltaRotation)),
            newRotation
        );
    }
}
