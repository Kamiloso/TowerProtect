using System;
using System.Linq;
using TowerProtect.Domain.Tools;
using TowerProtect.Domain.Game.Tiles;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Game.Tiles.Specific;

namespace TowerProtect.Domain.Game.Maps;

public abstract class Map
{
    public abstract int Range { get; }
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

    public Tile CreateTileAt(Vec2Int pos) => _table[pos] switch
    {
        "X" => new StoneTile(),
        "." => new AirTile(),
        _ => throw new InvalidOperationException()
    };
}