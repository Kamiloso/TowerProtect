using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Abstract;

public abstract class Tile
{
    public abstract TileType Type { get; }
    public virtual bool IsEntityPassable => false;
    public virtual bool IsProjectilePassable => false;

    public virtual TileData TileData => new(Type, 0);
}
