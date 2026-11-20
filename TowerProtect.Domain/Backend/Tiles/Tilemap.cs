using System;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Backend.Worldgen;
using AutoInterfaceAttributes;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Backend.Tiles;

partial interface ITilemap : IUpdateAware;

[AutoInterface]
public class Tilemap : ITilemap
{
    private readonly Area<Tile> _area;

    public event Action<Vec2Int, TileData>? OnTileUpdateEvent;
    public event Action<Vec2Int, EntityType>? OnSummonEvent;

    private bool _initialized;

    public Tilemap(IMap map, ITileFactory tileFactory)
    {
        _area = new Area<Tile>(map.Range);
        _area.Populate(pos =>
        {
            var tileType = map[pos];
            var tile = tileFactory.CreateTile(tileType);

            (tile as BreakableTile)?.OnBreakEvent += () => SetTile(pos, AirTile.Instance);

            return tile;
        });
    }

    public void Update(double deltaTime)
    {
        if (!_initialized)
        {
            _area.Iterate(pos =>
            {
                if (_area[pos] is not SpawnTile tile)
                    return;

                SetTile(pos, AirTile.Instance);
                OnSummonEvent?.Invoke(pos, tile.SpawnEntityType);
            });

            _initialized = true;
        }
    }

    public TilemapData CollectTilemapData()
        => TilemapData.FromArea(_area, t => t.TileData);
    

    public bool TryBreak(Vec2Int pos)
        => (_area[pos] as BreakableTile)?.TryBreak() ?? false;

    private void SetTile(Vec2Int pos, Tile tile)
    {
        _area[pos] = tile;
        OnTileUpdateEvent?.Invoke(pos, tile.TileData);
    }
}
