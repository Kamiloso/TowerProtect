using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class StoneTile : Tile
{
    private StoneTile() : base() { }
    public static StoneTile Instance { get; } = new();

    public override TileType Type => TileType.Stone;
}
