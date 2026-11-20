using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;
using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

internal sealed class PlayerEntity(Vec2 position, IEntityController controller)
    : RangedEntity(position, health: 5, controller)
{
    public override EntityType Type => EntityType.Player;
    public override double MovementSpeed => 32.0;
    public override bool IsRoot => true;
}
