using System;
using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Model.Tiles;

public readonly record struct TilemapData(int Range, string Base64)
{
    public static TilemapData FromArea<T>(Area<T> area, Func<T, TileData> mapper)
    {
        int ptr = 0;
        var bytes = new byte[area.Capacity * 2];

        area.Iterate(pos =>
        {
            var tileData = mapper(area[pos]);

            bytes[ptr++] = (byte)tileData.Type;
            bytes[ptr++] = tileData.Durability;
        });

        return new TilemapData(area.Range, Convert.ToBase64String(bytes));
    }

    public Func<Vec2Int, TileData> GetPopulator()
    {
        Dictionary<Vec2Int, TileData> dict = new(Area.Capacity(Range));

        int ptr = 0;
        byte[] bytes = Convert.FromBase64String(Base64);

        Area.Iterate(Range, pos =>
        {
            var tileType = (TileType)bytes[ptr++];
            byte durability = bytes[ptr++];

            dict.Add(pos, new TileData(tileType, durability));
        });

        return pos => dict.GetValueOrDefault(pos, TileData.Air);
    }
}
