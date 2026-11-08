using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Abstract;

public abstract class Tile
{
    public abstract TileType Type { get; }
    public virtual bool IsPassable => false;

    public virtual TileData TileData => new(Type, 0);
}
