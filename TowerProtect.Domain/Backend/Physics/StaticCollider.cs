using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Physics;

internal readonly record struct StaticCollider
{
    public required Vec2 Position { get; init; }
    public required Vec2 Size { get; init; }

    public Rect RectArea => new(Position, Size);
}
