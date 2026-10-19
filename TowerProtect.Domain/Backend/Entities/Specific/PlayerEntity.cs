using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;
using TowerProtect.Domain.Backend.Entities.Movement;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

public sealed class PlayerEntity(Vec2 position, IController controller)
    : RangedEntity(position, health: 5, controller), IUpdateAware
{
    public override EntityType Type => EntityType.Player;
    public override double MovementSpeed => 4.0;
}
