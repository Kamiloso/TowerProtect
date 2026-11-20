using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Backend.Entities.Physics;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Abstract.Dynamic.Controllable;

internal abstract class SummoningEntity(Vec2 position, byte health,
    IEntityController controller,
    PhysicalPropertiesOptional physicalProperties = default)
    : ControllableEntity(position, health, controller, physicalProperties)
{
    protected override void ShootingStep(double deltaTime) { }
}
