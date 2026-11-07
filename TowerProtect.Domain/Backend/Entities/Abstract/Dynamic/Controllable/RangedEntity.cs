using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class RangedEntity(Vec2 position, byte health,
    IEntityController controller)
    : ControllableEntity(position, health, controller)
{
    protected override void ShootingStep(double deltaTime)
        => Shoot(Position, Rotation);
}
