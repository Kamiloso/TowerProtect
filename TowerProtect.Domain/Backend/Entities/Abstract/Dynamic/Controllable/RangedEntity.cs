using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class RangedEntity(Vec2 position, byte health, EntityType projectileType,
    IEntityController controller,
    PhysicalProperties physicalProperties)
    : ControllableEntity(position, health, controller, physicalProperties)
{
    protected EntityType ProjectileType { get; } = projectileType;

    protected override void ShootingStep(double deltaTime)
        => Shoot(Position, Rotation, ProjectileType);
}
