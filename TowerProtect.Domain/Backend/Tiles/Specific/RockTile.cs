using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class RockTile(byte durability) : BreakableTile(durability)
{
    public override TileType Type => TileType.Rock;
}
