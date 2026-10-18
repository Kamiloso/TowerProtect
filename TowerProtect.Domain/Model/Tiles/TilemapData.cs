using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Model.Tiles;

public readonly record struct TilemapData(
    int Range,
    ImmutableArray<PosTileData> PosTiles)
{
    public static TilemapData FromArea<T>(Area<T> area, Func<Vec2Int, T, PosTileData> mapper)
    {
        var list = new List<PosTileData>(area.Capacity);
        area.Iterate(pos => list.Add(mapper(pos, area[pos])));
        return new TilemapData(area.Range, [.. list]);
    }

    public Func<Vec2Int, TileData> GetPopulator()
    {
        Dictionary<Vec2Int, TileData> dict = PosTiles
            .DistinctBy(pt => pt.Pos)
            .ToDictionary(pt => pt.Pos, pt => pt.Tile);

        return pos => dict.GetValueOrDefault(pos, TileData.Air);
    }
}
