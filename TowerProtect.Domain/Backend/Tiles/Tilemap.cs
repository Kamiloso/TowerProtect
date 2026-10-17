using System;
using TowerProtect.Domain.Tools;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles;

public class Tilemap : IUpdateAware
{
    private readonly Area<Tile> _area;

    public event Action<PosTileData>? TileUpdateEvent;

    public Tilemap(int range, Func<Vec2Int, Tile> populator)
    {
        _area = new Area<Tile>(range);
        _area.Populate(pos =>
        {
            Tile tile = populator(pos);
            tile.OnBreakEvent += () =>
            {
                _area[pos] = new AirTile();
                TileUpdateEvent?.Invoke(_area[pos].GetPosTileData(pos));
            };
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

    public bool TryBreak(Vec2Int pos) => _area[pos].TryBreak();
}
