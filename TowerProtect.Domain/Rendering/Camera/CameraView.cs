using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Rendering.Camera;

[AutoInterface]
public class CameraView(CameraConfig cfg) : ICameraView
{
    public Vec2 Center { get; set; }
    public double Zoom { get; set; }

    public Rect VisionArea => new(Center, AspectRatio >= 1.0
        ? Side * new Vec2(AspectRatio, 1.0)
        : Side * new Vec2(1.0, 1.0 / AspectRatio));

    public Rect ExtendedVisionArea(double deltaSide)
        => new(Center, (VisionArea.Size + deltaSide * Vec2.One).Abs);

    public double AspectRatio
    {
        private get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0.0);
            field = value;
        }
    } = 1.0;

    private double Side => cfg.DefaultSide / Math.Pow(2.0, Zoom);

    public void ApplyTransform(Vec2 center, double zoom)
    {
        Center = center;
        Zoom = zoom;
    }

    public void ResetTransform() => ApplyTransform(Vec2.Zero, 0.0);

    public bool IsCulled(Vec2 position, double deltaSide)
    {
        Vec2 delta = (Center - position).Abs;
        Vec2 limit = ExtendedVisionArea(deltaSide).Size / 2.0;

        return delta.x > limit.x || delta.y > limit.y;
    }
}
