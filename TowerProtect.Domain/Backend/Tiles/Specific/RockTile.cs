using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class RockTile() : Tile(durability: 30)
{
    public override TileType Type => TileType.Rock;
    public override bool IsBreakable => true;
}
