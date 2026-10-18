using System;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Model.Tiles;
using TowerProtect.Domain.Backend.Maps;
using AutoInterfaceAttributes;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.Domain.Backend.Tiles;

partial interface ITilemap;

[AutoInterface]
public class Tilemap : ITilemap
{
    private readonly Area<Tile> _area;

    public event Action<PosTileData>? TileUpdateEvent;

    public Tilemap(IMap map)
    {
        _area = new Area<Tile>(map.Range);
        _area.Populate(pos =>
        {
            Tile tile = map.CreateTileAt(pos);
            tile.OnBreakEvent += () =>
            {
                _area[pos] = new AirTile();
                TileUpdateEvent?.Invoke(_area[pos].GetPosTileData(pos));
            };
            return tile;
        });
    }

    public TilemapData CollectTilemapData()
        => TilemapData.FromArea(_area, (pos, t) => t.GetPosTileData(pos));

    public bool TryBreak(Vec2Int pos) => _area[pos].TryBreak();
}
