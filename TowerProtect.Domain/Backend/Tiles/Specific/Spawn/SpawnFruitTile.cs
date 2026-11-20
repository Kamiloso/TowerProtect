using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific.Spawn;

internal sealed class SpawnFruitTile : SpawnTile
{
    private SpawnFruitTile() : base() { }
    public static SpawnFruitTile Instance { get; } = new();

    public override TileType Type => TileType.SpawnFruit;
    public override EntityType SpawnEntityType => EntityType.Fruit;
}
