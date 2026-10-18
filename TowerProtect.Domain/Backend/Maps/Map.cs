using System;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Model.Tiles;
using AutoInterfaceAttributes;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.Domain.Backend.Maps;

[AutoInterface]
public abstract class Map : IMap
{
    public abstract int Range { get; }
    public int Size => 2 * Range + 1;
    public int Capacity => Size * Size;

    private readonly Area<TileType> _table;

    public Map()
    {
        _table = new Area<TileType>(Range, out int size);

        TileType[,] tiles = GenerateTiles();
        {
            int dim1 = tiles.GetLength(0);
            int dim2 = tiles.GetLength(1);

            if (dim1 != size || dim2 != size)
            {
                throw new InvalidOperationException($"Invalid dimensions: {dim1}x{dim2}");
            }
        }

        _table.Populate(pos =>
        {
            int i = Range - pos.y;
            int j = pos.x + Range;

            return tiles[i, j];
        });
    }

    public Tile CreateTileAt(Vec2Int pos)
    {
        TileType tile = _table[pos];
        return tile switch
        {
            TileType.Air => new AirTile(),
            TileType.Stone => new StoneTile(),
            TileType.Spawner => new SpawnerTile(),
            TileType.Water => new WaterTile(),
            TileType.Bedrock => new BedrockTile(),
            TileType.Rock => new RockTile(),
            _ => throw new InvalidOperationException($"Invalid tile type: {tile}")
        };
    }

    protected abstract TileType[,] GenerateTiles();
}
