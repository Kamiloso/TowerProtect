namespace TowerProtect.Domain.Model.Gameplay.Tiles;

public readonly record struct TileData(
    TileType Type,
    byte Durability)
{
    public static TileData Air => new(TileType.Air, 0);
}
