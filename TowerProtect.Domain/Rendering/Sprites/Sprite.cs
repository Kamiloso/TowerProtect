using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering.Model;

namespace TowerProtect.Domain.Rendering.Sprites;

public sealed record Sprite(
    string TextureName,
    Rect TextureArea,
    Layer DisplayLayer,
    double Rotation = 0.0,
    int SortEntropy = 0) : IRenderable
{
    public Vec2 Position => TextureArea.Center;
    public IEnumerable<Sprite> Sprites => [this];

    public Sprite(string textureName, Vec2 position, Layer displayLayer, double rotation = 0.0, int sortEntropy = 0)
        : this(textureName, new Rect(position), displayLayer, rotation, sortEntropy) { }

    public Sprite(string textureName, double side, Layer displayLayer, double rotation = 0.0, int sortEntropy = 0)
        : this(textureName, new Rect(side), displayLayer, rotation, sortEntropy) { }

    public Sprite(string textureName, Layer displayLayer, double rotation = 0.0, int sortEntropy = 0)
        : this(textureName, new Rect(), displayLayer, rotation, sortEntropy) { }

    public IRenderable Move(Vec2 deltaPosition)
    {
        if (deltaPosition == Vec2.Zero)
            return this;

        Vec2 newCenter = Position + deltaPosition;
        return this with { TextureArea = TextureArea with { Center = newCenter } };
    }

    public IRenderable Rotate(double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        double newRotation = (Rotation + deltaRotation) % (Math.PI * 2);
        return this with { Rotation = newRotation };
    }

    public IRenderable MoveLocal(Vec2 deltaPositionLocal)
    {
        if (deltaPositionLocal == Vec2.Zero)
            return this;

        Vec2 newCenter = Position + deltaPositionLocal.Rotate(Rotation);
        return this with { TextureArea = TextureArea with { Center = newCenter } };
    }

    public IRenderable RotateAround(Vec2 pivot, double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        Vec2 offset = Position - pivot;
        Vec2 newCenter = pivot + offset.Rotate(deltaRotation);
        double newRotation = (Rotation + deltaRotation) % (Math.PI * 2);

        return this with
        {
            TextureArea = TextureArea with { Center = newCenter },
            Rotation = newRotation
        };
    }
}
