using System;
using TowerProtect.Domain.Tools;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Game.Tiles.Specific;

namespace TowerProtect.Domain.Game.Tiles;

public class Tilemap : IUpdateAware
{
    private readonly Area<Tile> _area;

    public Tilemap(int range, Func<Vec2Int, Tile> populator)
    {
        _area = new Area<Tile>(range);
        _area.Populate(pos =>
        {
            Tile tile = populator(pos);
            tile.OnBreakEvent += () => _area[pos] = new AirTile();
            return tile;
        });
    }

    public void Update(double deltaTime)
    {
        _area.Iterate(pos =>
        {
            var updateAware = _area[pos] as IUpdateAware;
            updateAware?.Update(deltaTime);
        });
    }

    public bool TryReplace(Vec2Int pos, Tile newTile, int power = 0)
    {
        int breakPower = Math.Max(power, newTile.Toughness);
        return _area[pos].TryBreak(breakPower);
    }

    public bool TryBreak(Vec2Int pos, int power)
    {
        return _area[pos].TryBreak(power);
    }
}
