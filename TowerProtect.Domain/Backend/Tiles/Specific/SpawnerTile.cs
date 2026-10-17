using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class SpawnerTile : Tile
{
    public override TileType Type => TileType.Spawner;
}
