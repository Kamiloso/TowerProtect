using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific.Spawn;

internal sealed class SpawnAnimalTile : SpawnTile
{
    private SpawnAnimalTile() : base() { }
    public static SpawnAnimalTile Instance { get; } = new();

    public override TileType Type => TileType.SpawnAnimal;
    public override EntityType SpawnEntityType => EntityType.Animal;
}
