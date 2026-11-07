using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class ControllableEntity(Vec2 position, byte health,
    IEntityController controller)
    : DynamicEntity(position, 0f, health), IUpdateAware
{
    protected IEntityController Controller { get; } = controller;
    
    public override bool IsDamageable => true;
    public override bool IsFlying => false;
    public override bool IsRendered => _inputReceived && base.IsRendered;

    private bool _inputReceived;

    public virtual void Update(double deltaTime)
    {
        if (Controller.InputReceived)
        {
            Rotation = Controller.Rotation;

            if (Controller.IsMoving)
                MovementStep(deltaTime);

            if (Controller.IsShooting)
                ShootingStep(deltaTime);

            _inputReceived = true;
        }
    }

    protected abstract void ShootingStep(double deltaTime);
}
