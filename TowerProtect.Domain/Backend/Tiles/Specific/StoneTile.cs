using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class StoneTile : Tile
{
    public override TileType Type => TileType.Stone;
}
