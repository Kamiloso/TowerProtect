using System;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities;

public abstract class Entity(Vec2 position, double rotation, int health)
{
    public Guid Guid { get; } = Guid.NewGuid();

    public Vec2 Position { get; protected set; } = position;
    public double Rotation { get; protected set; } = rotation;

    public int Health { get; private set; } = health;
    public bool IsAlive { get; private set; } = true;

    public event Action? OnDeath;

    public bool ApplyDamage(int damage)
    {
        if (IsAlive && (Health -= damage) <= 0)
        {
            BeforeDeath();

            OnDeath?.Invoke();
            IsAlive = false;

            return true;
        }

        return false;
    }

    protected virtual void BeforeDeath() { }
}
