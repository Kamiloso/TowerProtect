using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class MagicStoneTile : Tile
{
    private MagicStoneTile() : base() { }
    public static MagicStoneTile Instance { get; } = new();

    public override TileType Type => TileType.MagicStone;
}
