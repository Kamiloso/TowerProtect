using TowerProtect.Domain.Backend.Physics;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic;

internal abstract class DynamicEntity(Vec2 position, double rotation, byte health,
    DynamicCollider collider)
    : Entity(position, rotation, health), IUpdateAware
{
    public DynamicCollider Collider { get; } = collider;

    public abstract double MovementSpeed { get; }

    public virtual void Update(double deltaTime)
    {
        Collider.Update(deltaTime);
        Position = Collider.Position;
    }
}
