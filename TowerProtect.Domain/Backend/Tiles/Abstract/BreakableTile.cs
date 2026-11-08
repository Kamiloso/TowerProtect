using System;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Abstract;

public abstract class BreakableTile(byte durability) : Tile
{
    public byte Durability { get; private set; } = durability;
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
