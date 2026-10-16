using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Rendering;

public readonly record struct Sprite(string TextureName, Rect TextureArea) : IRenderable<Sprite>
{
    public Vec2 Position => TextureArea.Center;
    public double Rotation { get; private init; } = 0.0;
    public IEnumerable<Sprite> Sprites => [this];

    public Sprite Move(Vec2 deltaPosition)
    {
        if (deltaPosition == Vec2.Zero)
            return this;

        Vec2 newCenter = Position + deltaPosition;
        Rect newTextureArea = TextureArea with { Center = newCenter };

        return this with { TextureArea = newTextureArea };
    }

    public Sprite Rotate(double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        double newRotation = Rotation + deltaRotation;
        newRotation %= Math.PI * 2;

        return this with { Rotation = newRotation };
    }

    public Sprite MoveLocal(Vec2 deltaPositionLocal)
    {
        if (deltaPositionLocal == Vec2.Zero)
            return this;

        Vec2 newCenter = Position + deltaPositionLocal.Rotate(Rotation);
        Rect newTextureArea = TextureArea with { Center = newCenter };

        return this with { TextureArea = newTextureArea };
    }

    public Sprite RotateAround(Vec2 pivot, double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        Vec2 offset = Position - pivot;
        Vec2 rotatedOffset = offset.Rotate(deltaRotation);
        Vec2 newCenter = pivot + rotatedOffset;

        Rect newTextureArea = TextureArea with { Center = newCenter };

        double newRotation = Rotation + deltaRotation;
        newRotation %= Math.PI * 2;

        return this with
        {
            TextureArea = newTextureArea,
            Rotation = newRotation
        };
    }
}
