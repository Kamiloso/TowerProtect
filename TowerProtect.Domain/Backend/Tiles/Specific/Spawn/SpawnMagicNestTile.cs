using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific.Spawn;

internal sealed class SpawnMagicNestTile : SpawnTile
{
    private SpawnMagicNestTile() : base() { }
    public static SpawnMagicNestTile Instance { get; } = new();

    public override TileType Type => TileType.SpawnMagicNest;
    public override EntityType SpawnEntityType => EntityType.MagicNest;
}
