using TowerProtect.Domain.Backend.Entities.Movement;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

public abstract class ControllableEntity(Vec2 position, byte health, IController controller)
    : DynamicEntity(position, 0f, health), IUpdateAware
{
    public override bool IsDamageable => true;
    public override bool IsFlying => false;

    public virtual void Update(double deltaTime)
    {
        Rotation = controller.Rotation;

        if (controller.IsMoving)
            MovementStep(deltaTime);

        if (controller.IsShooting)
            ShootingStep(deltaTime);
    }

    protected abstract void ShootingStep(double deltaTime);
}
