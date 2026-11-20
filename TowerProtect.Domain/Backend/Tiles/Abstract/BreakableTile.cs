using System;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Abstract;

public abstract class BreakableTile(Random rng, (byte Min, byte Max) durabilityRange) : Tile
{
    public byte Durability { get; private set; } = (byte)rng.Next(durabilityRange.Min, durabilityRange.Max + 1);
    public bool Exists { get; private set; } = true;

    public override TileData TileData => new(Type, Durability);

    public event Action? OnBreakEvent;

    public bool TryBreak()
    {
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
