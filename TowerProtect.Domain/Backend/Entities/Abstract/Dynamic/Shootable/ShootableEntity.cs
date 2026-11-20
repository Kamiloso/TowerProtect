using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Shootable;

internal abstract class ShootableEntity(Vec2 position, double rotation,
    PhysicalPropertiesOptional physicalProperties = default)
    : DynamicEntity(position, rotation, 0, collider: physicalProperties
        .UseTemplate(DefaultPhysicalProperties)
        .CreateCollider(position))
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

    private static readonly PhysicalProperties DefaultPhysicalProperties = new()
    {
        Size = new Vec2(0.5),
        Drag = 0.0,
        Bounciness = 1.0,
    };
}
