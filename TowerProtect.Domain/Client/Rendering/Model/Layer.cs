using System;
using System.Linq;

namespace TowerProtect.Domain.Client.Rendering.Model;

public enum Layer
{
    Effect = 800,
    Solid = 700,
    Player = 600,
    Entity = 500,
    Projectile = 400,
    EnemyProjectile = 300,
    Unsolid = 200,
    BelowAir = 100,
    Default = default,
    Background = -100,
}

public static class LayerExtensions
{
    private static readonly int MaxSubLayer;
    private static readonly int Min;
    private static readonly int Max;

    static LayerExtensions()
    {
        var values = (int[])Enum.GetValuesAsUnderlyingType<Layer>();

        MaxSubLayer = (values
            .SelectMany(a => values, (a, b) => a - b)
            .Where(diff => diff > 0)
            .Min() - 1) / 2;

        Min = values.Min() - MaxSubLayer;
        Max = values.Max() + MaxSubLayer;
    }

    public static float ToDepthLayer(this Layer self)
        => (float)((int)self - Min) / (Max - Min);

    public static Layer SubLayer(this Layer self, int sub)
    {
        if (sub < -MaxSubLayer || sub > MaxSubLayer)
            throw new ArgumentOutOfRangeException(nameof(sub));

        return (Layer)((int)self + sub);
    }
}
