using TowerProtect.Domain.Backend.Entities.Movement;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

public abstract class MeleeEntity(Vec2 position, byte health, IController controller)
    : ControllableEntity(position, health, controller)
{
    protected override void ShootingStep(double deltaTime) { }
}
