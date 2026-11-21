using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

internal sealed class ProjectileEntity(Vec2 position, double rotation)
    : ShootableEntity(
        position, rotation,
        physicalProperties: new() { Size = ConstType.GetSize() })
{
    public override EntityType Type => ConstType;
    public override double MovementSpeed => 16.0;

    private const EntityType ConstType = EntityType.Projectile;
}
