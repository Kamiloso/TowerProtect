using System;
using System.Linq;
using TowerProtect.Domain.Tools;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Backend.Tiles.Specific;

namespace TowerProtect.Domain.Backend.Maps;

public abstract class Map
{
    public abstract int Range { get; }
    public int Size => 2 * Range + 1;
    public int Capacity => Size * Size;

    protected abstract string Template { get; }

    private readonly Area<string> _table;

    public Map()
    {
        _table = new Area<string>(Range, out int size);

        string[] tokens = [.. Template.Split('\n')
            .Select(l => l.Trim())
            .SelectMany(l => l.Split())];

        if (tokens.Length != size * size)
            throw new InvalidOperationException();

        _table.Populate(pos =>
        {
            int i = Range - pos.y;
            int j = pos.x + Range;
            
            return tokens[i * size + j];
        });
    }

    public Tile CreateTileAt(Vec2Int pos)
    {
        string tile = _table[pos];
        return tile switch
        {
            "." => new AirTile(),
            "#" => new StoneTile(),
            "O" => new CrushTile(),
            "!" => new SpawnerTile(),
            "+" => new WaterTile(),
            "B" => new BedrockTile(),
            "*" => new RockTile(),
            _ => throw new InvalidOperationException($"Invalid tile: {tile}")
        };
    }
}