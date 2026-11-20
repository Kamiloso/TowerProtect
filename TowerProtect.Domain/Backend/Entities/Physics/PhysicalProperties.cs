using TowerProtect.Domain.Backend.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Physics;

internal readonly record struct PhysicalProperties
{
    public required Vec2 Size { get; init; }
    public required double Drag { get; init; }
    public required double Bounciness { get; init; }

    public DynamicCollider CreateCollider(Vec2 position) => new()
    {
        Position = position,
        Size = Size,
        Drag = Drag,
        Bounciness = Bounciness,
    };
}
