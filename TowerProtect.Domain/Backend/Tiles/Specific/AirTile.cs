using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class AirTile : Tile
{
    public override TileType Type => TileType.Air;
    public override bool IsPassable => true;
}
