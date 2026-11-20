using System;
using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using static TowerProtect.Domain.Backend.Physics.PhysicsConst;

namespace TowerProtect.Domain.Backend.Physics;

public record DynamicCollider : IUpdateAware
{
    public required Vec2 Position { get; set; }
    public required Vec2 Size { get; set; }
    public required double Drag { get; set; }
    public required double Bounciness { get; set; }

    public Vec2 Velocity { get; set; } = Vec2.Zero;
    public Rect RectArea => new(Position, Size);

    public event Action<DynamicCollider, double>? OnUpdateEvent;

    private IReadOnlyList<StaticCollider> _colliders = [];

    public void Update(double deltaTime)
    {
        OnUpdateEvent?.Invoke(this, deltaTime);
    }

    internal void Move(double deltaTime, IReadOnlyList<StaticCollider> colliders)
    {
        int iterations = MaxBounceIterations;

        while (deltaTime > 0.0 && iterations-- > 0)
        {
            Vec2 p1 = Position;
            Vec2 p2 = Position + deltaTime * Velocity;

            _colliders = colliders;
            _colliders = WouldCollideAt(p1) ? [] : colliders;

            var (finalPosition, timeElapsed, isHorizontal, isVertical) = MoveStraight(p1, p2, deltaTime);

            Position = finalPosition;
            Velocity *= Math.Exp(-Drag * timeElapsed);

            if (isHorizontal)
            {
                Velocity = new Vec2(-Velocity.x * Bounciness, Velocity.y);
            }

            if (isVertical)
            {
                Velocity = new Vec2(Velocity.x, -Velocity.y * Bounciness);
            }

            deltaTime -= timeElapsed;
        }
    }

    private Collision MoveStraight(Vec2 p1, Vec2 p2, double deltaTime)
    {
        if (!WouldCollideAt(p2))
        {
            return new Collision(p2, deltaTime);
        }

        if (Vec2.Distance(p1, p2) <= SpatialEpsilon)
        {
            Vec2 delta = p2 - p1;

            Vec2 pH = p1 + new Vec2(delta.x, 0.0);
            Vec2 pV = p1 + new Vec2(0.0, delta.y);

            bool isHorizontal = WouldCollideAt(pH);
            bool isVertical = WouldCollideAt(pV);
            bool isNone = !isHorizontal && !isVertical;

            isHorizontal |= isNone;
            isVertical |= isNone;

            return new Collision(p1, 0.0, isHorizontal, isVertical);
        }

        Vec2 pM = (p1 + p2) / 2.0;
        double hDeltaTime = deltaTime / 2.0;

        if (WouldCollideAt(pM))
        {
            return MoveStraight(p1, pM, hDeltaTime);
        }
        else
        {
            var first = new Collision(pM, hDeltaTime);
            var second = MoveStraight(pM, p2, hDeltaTime);

            return first.Stack(second);
        }
    }

    private bool WouldCollideAt(Vec2 position)
    {
        foreach (var collider in _colliders)
        {
            Rect expandedRect = new(collider.Position, collider.Size + Size);

            if (expandedRect.Contains(position))
                return true;
        }

        return false;
    }
}
