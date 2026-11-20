using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Physics;

internal readonly record struct PhysicalPropertiesOptional
{
    public Vec2? Size { get; init; }
    public double? Drag { get; init; }
    public double? Bounciness { get; init; }

    public PhysicalProperties UseTemplate(PhysicalProperties template) => new()
    {
        Size = Size ?? template.Size,
        Drag = Drag ?? template.Drag,
        Bounciness = Bounciness ?? template.Bounciness,
    };
}
