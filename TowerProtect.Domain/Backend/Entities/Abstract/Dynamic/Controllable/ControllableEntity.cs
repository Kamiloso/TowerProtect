using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class ControllableEntity(Vec2 position, byte health,
    IEntityController controller,
    PhysicalProperties physicalProperties)
    : DynamicEntity(
        position, rotation: 0f, health,
        collider: physicalProperties.Compile(position, new PhysicalProperties()
        {
            Drag = 4.0,
            Bounciness = 0.0,
        }))
{
    public IEntityController Controller { get; } = controller;

    public override bool IsDamageable => true;
    public override bool IsRendered => _inputReceived && base.IsRendered;

    private bool _inputReceived;

    public override void Update(double deltaTime)
    {
        if (Controller.InputReceived)
        {
            Rotation = Controller.Rotation;

            if (Controller.IsMoving)
            {
                MovingStep(deltaTime);
            }

            if (Controller.IsShooting)
            {
                ShootingStep(deltaTime);
            }

            _inputReceived = true;
        }

        base.Update(deltaTime);
    }

    protected virtual void MovingStep(double deltaTime)
    {
        Collider.Velocity += PointVector * MovementSpeed * deltaTime;
    }

    protected virtual void ShootingStep(double deltaTime) { }
}
