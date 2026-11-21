using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class SummoningEntity(Vec2 position, byte health,
    IEntityController controller,
    PhysicalProperties physicalProperties)
    : ControllableEntity(position, health, controller, physicalProperties);
