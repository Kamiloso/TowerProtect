using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Physics;

internal readonly record struct Collision(
    Vec2 FinalPosition, double TimeElapsed, bool IsHorizontal, bool IsVertical)
{
    public Collision(Vec2 finalPosition, double timeElapsed)
        : this(finalPosition, timeElapsed, false, false) { }

    public Collision Stack(Collision next) => new(
        FinalPosition: next.FinalPosition,
        TimeElapsed: TimeElapsed + next.TimeElapsed,
        IsHorizontal: IsHorizontal || next.IsHorizontal,
        IsVertical: IsVertical || next.IsVertical
    );
}
