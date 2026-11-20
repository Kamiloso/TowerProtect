using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

internal sealed class SpawnMonsterTile : SpawnTile
{
    private SpawnMonsterTile() : base() { }
    public static SpawnMonsterTile Instance { get; } = new();

    public override TileType Type => TileType.SpawnMonster;
    public override EntityType SpawnEntityType => EntityType.Monster;
}
