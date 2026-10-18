using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

public sealed class PlayerEntity(Vec2 position)
    : Entity(position, 0.0, health: 100), IUpdateAware
{
    public double Speed => 4.0;

    public void Update(double deltaTime)
    {
        double angle = Rotation;
        double magnitude = deltaTime * Speed;

        Position += magnitude * Vec2.Right.Rotate(angle);
    }
}
