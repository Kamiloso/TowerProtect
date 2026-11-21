using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;
using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

internal sealed class PlayerEntity(Vec2 position, IEntityController controller)
    : RangedEntity(
        position, health: 5, projectileType: EntityType.Projectile,
        controller, physicalProperties: new() { Size = ConstType.GetSize() })
{
    public override EntityType Type => ConstType;
    public override double MovementSpeed => 64.0;
    public override bool IsRoot => true;

    private const EntityType ConstType = EntityType.Player;
}
