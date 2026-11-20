using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class ControllableEntity(Vec2 position, byte health,
    IEntityController controller,
    PhysicalPropertiesOptional physicalProperties = default)
    : DynamicEntity(position, rotation: 0f, health, collider: physicalProperties
        .UseTemplate(DefaultPhysicalProperties)
        .CreateCollider(position))
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

    protected abstract void ShootingStep(double deltaTime);

    private static readonly PhysicalProperties DefaultPhysicalProperties = new()
    {
        Size = new Vec2(0.8),
        Drag = 0.15,
        Bounciness = 0.0,
    };
}
