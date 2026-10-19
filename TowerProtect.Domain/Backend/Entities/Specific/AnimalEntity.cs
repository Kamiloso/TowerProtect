using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;
using TowerProtect.Domain.Backend.Entities.Movement;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

public sealed class AnimalEntity(Vec2 position, IController controller)
    : MeleeEntity(position, health: 1, controller), IUpdateAware
{
    public override EntityType Type => EntityType.Animal;
    public override double MovementSpeed => 2.0;
}
