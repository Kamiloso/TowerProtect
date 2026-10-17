using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class CrushTile() : Tile(durability: 15)
{
    public override TileType Type => TileType.Crush;
    public override bool IsBreakable => true;
}
