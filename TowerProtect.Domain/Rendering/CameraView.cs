using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Rendering;

[AutoInterface]
public class CameraView : ICameraView
{
    public Vec2 Center { get; set; } = Vec2.Zero;
    public double Zoom { get; set; } = 0.0;

    private double Side => Config.DefaultCameraSide / Math.Pow(2.0, Zoom);

    public double AspectRatio
    {
        private get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0.0);
            field = value;
        }
    } = 1.0;

    public Rect VisionArea => new(Center, AspectRatio >= 1.0
        ? Side * new Vec2(AspectRatio, 1.0)
        : Side * new Vec2(1.0, 1.0 / AspectRatio)
    );

    public void ApplyTransform(Vec2 center, double zoom)
    {
        Center = center;
        Zoom = zoom;
    }

    public void ResetTransform() => ApplyTransform(Vec2.Zero, 0.0);

    public bool IsCulled(IRenderable renderable)
    {
        double baseLimit = Side / 2.0;
        baseLimit *= 1.5;

        Vec2 limit = AspectRatio >= 1.0
            ? baseLimit * new Vec2(AspectRatio, 1.0)
            : baseLimit * new Vec2(1.0, 1.0 / AspectRatio);

        Vec2 delta = new(
            Math.Abs(Center.x - renderable.Position.x),
            Math.Abs(Center.y - renderable.Position.y)
        );

        return delta.x > limit.x || delta.y > limit.y;
    }
}
