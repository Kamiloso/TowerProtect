using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Backend.Tiles.Abstract;

internal abstract class SpawnTile() : Tile
{
    public abstract EntityType SpawnEntityType { get; }
}
