using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Model.Tiles;

public readonly record struct PosTileData(
    Vec2Int Pos,
    TileData Tile);
