using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering.Model;

namespace TowerProtect.Domain.Rendering.Sprites;

public sealed class Sprite(
    string textureName,
    Rect textureArea,
    Layer displayLayer,
    double rotation = 0.0) : IRenderable
{
    public Vec2 Position => textureArea.Center;
    public double Rotation => rotation;
    public IEnumerable<Sprite> Sprites => [this];

    public string TextureName => textureName;
    public Rect TextureArea => textureArea;
    public Layer DisplayLayer => displayLayer;

    public Sprite(string textureName, Vec2 position, Layer displayLayer, double rotation = 0.0)
        : this(textureName, new Rect(position), displayLayer, rotation) { }

    public Sprite(string textureName, double side, Layer displayLayer, double rotation = 0.0)
        : this(textureName, new Rect(side), displayLayer, rotation) { }

    public Sprite(string textureName, Layer displayLayer, double rotation = 0.0)
        : this(textureName, new Rect(), displayLayer, rotation) { }

    public IRenderable Move(Vec2 deltaPosition)
    {
        if (deltaPosition == Vec2.Zero)
            return this;

        return WithCenter(Position + deltaPosition);
    }

    public IRenderable Rotate(double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        return WithRotation((rotation + deltaRotation) % (Math.PI * 2));
    }

    public IRenderable MoveLocal(Vec2 deltaPositionLocal)
    {
        if (deltaPositionLocal == Vec2.Zero)
            return this;

        return WithCenter(Position + deltaPositionLocal.Rotate(rotation));
    }

    public IRenderable RotateAround(Vec2 pivot, double deltaRotation)
    {
        if (deltaRotation == 0.0)
            return this;

        Vec2 offset = Position - pivot;
        Vec2 rotatedOffset = offset.Rotate(deltaRotation);
        Vec2 newCenter = pivot + rotatedOffset;

        return WithCenterAndRotation(newCenter, (rotation + deltaRotation) % (Math.PI * 2));
    }

    private Sprite WithCenter(Vec2 newCenter) =>
        new(textureName, textureArea with { Center = newCenter }, displayLayer, rotation);

    private Sprite WithRotation(double newRotation) =>
        new(textureName, textureArea, displayLayer, newRotation);

    private Sprite WithCenterAndRotation(Vec2 newCenter, double newRotation) =>
        new(textureName, textureArea with { Center = newCenter }, displayLayer, newRotation);
}
