using System;

namespace TowerProtect.Domain.Game.Tiles;

public abstract class Tile(int durability)
{
    public abstract int Toughness { get; }

    public int Durability { get; private set; } = durability;
    public bool Exists { get; private set; } = true;

    public event Action? OnBreakEvent;

    public bool TryBreak(int breakPower)
    {
        if (breakPower <= Toughness)
        {
            return false;
        }

        if (Exists && --Durability <= 0)
        {
            BeforeBreak();

            OnBreakEvent?.Invoke();
            Exists = false;

            return true;
        }

        return false;
    }

    protected virtual void BeforeBreak() { }
}
