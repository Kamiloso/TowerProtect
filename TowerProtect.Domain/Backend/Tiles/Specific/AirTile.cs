using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class AirTile : Tile
{
    private AirTile() : base() { }
    public static AirTile Instance { get; } = new();

    public override TileType Type => TileType.Air;
    public override bool IsEntityPassable => true;
    public override bool IsProjectilePassable => true;
}
