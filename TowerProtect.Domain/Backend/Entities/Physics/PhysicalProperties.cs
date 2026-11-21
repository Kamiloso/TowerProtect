using System;
using TowerProtect.Domain.Backend.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Physics;

internal readonly record struct PhysicalProperties
{
    public Vec2? Size { get; init; }
    public double? Drag { get; init; }
    public double? Bounciness { get; init; }

    public DynamicCollider Compile(Vec2 position, PhysicalProperties template) => new()
    {
        Position = position,
        Size = Size ?? template.Size ?? throw Err(nameof(Size)),
        Drag = Drag ?? template.Drag ?? throw Err(nameof(Drag)),
        Bounciness = Bounciness ?? template.Bounciness ?? throw Err(nameof(Bounciness)),
    };

    private static Exception Err(string name)
        => throw new InvalidOperationException($"Physical property not provided: {name}");
}
