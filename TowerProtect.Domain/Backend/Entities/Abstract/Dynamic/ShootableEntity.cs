using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic;

internal abstract class ShootableEntity(Vec2 position, double rotation,
    PhysicalProperties physicalProperties)
    : DynamicEntity(
        position, rotation, health: 0,
        collider: physicalProperties.Compile(position, new PhysicalProperties()
        {
            Drag = 0.0,
            Bounciness = 1.0,
        }))
{
    public sealed override bool IsDamageable => false;

    private bool _initialized;

    public override void Update(double deltaTime)
    {
        if (!_initialized)
        {
            Collider.Velocity = PointVector * MovementSpeed;
            _initialized = true;
        }

        base.Update(deltaTime);
    }
}
