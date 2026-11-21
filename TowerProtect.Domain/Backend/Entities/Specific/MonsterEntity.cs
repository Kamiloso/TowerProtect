using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;
using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

internal sealed class MonsterEntity(Vec2 position, IEntityController controller)
    : MeleeEntity(
        position, health: 5,
        controller, physicalProperties: new() { Size = ConstType.GetSize() })
{
    public override EntityType Type => ConstType;
    public override double MovementSpeed => 8.0;

    private const EntityType ConstType = EntityType.Monster;
}
