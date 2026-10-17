using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Specific;

internal sealed class PlayerEntity(Vec2 position)
    : Entity(position, 0.0, Initial.Health), IUpdateAware
{
    public int Level { get; private set; } = 0;
    public double Speed => Level switch
    {// TODO: do something with this switch
        <= 0 => 4.0,
        <= 5 => 5.0,
        <= 10 => 6.0,
        <= 15 => 7.0,
        _ => 8.0
    };

    public void Update(double deltaTime)
    {
        double angle = Rotation;
        double magnitude = deltaTime * Speed;

        Position += magnitude * Vec2.Right.Rotate(angle);
    }

    public static class Initial
    {
        public const int Health = 100;
    }
}
