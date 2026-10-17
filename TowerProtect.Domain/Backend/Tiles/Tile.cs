using System;
using TowerProtect.Domain.Model.Tiles;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Tiles;

public abstract class Tile(int durability = 0)
{
    public abstract TileType Type { get; }
    public virtual bool IsBreakable => false;
    public virtual bool IsPassable => false;

    public int Durability { get; private set; } = durability;
    public bool Exists { get; private set; } = true;

    public event Action? OnBreakEvent;

    public bool TryBreak()
    {
        if (IsBreakable && Exists && --Durability <= 0)
        {
            BeforeBreak();

            OnBreakEvent?.Invoke();
            Exists = false;

            return true;
        }

        return false;
    }

    public TileData GetTileData() => new(Type, Durability);
    public PosTileData GetPosTileData(Vec2Int pos) => new(pos, GetTileData());

    protected virtual void BeforeBreak() { }
}
