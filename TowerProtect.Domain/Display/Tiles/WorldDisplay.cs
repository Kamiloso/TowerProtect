using System.Collections.Generic;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Tiles;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.Domain.Display.Tiles;

public class WorldDisplay : ISpriteRenderingAware
{
    private const int TypicalMapRange = 24;

    private BackgroundDisplay _background = new(TypicalMapRange);
    private TilemapDisplay? _tilemap;

    private bool _initialized;

    public void Initialize(TilemapData tilemapData)
    {
        if (_initialized) return;
        _initialized = true;

        int range = tilemapData.Range;
        var populator = tilemapData.GetPopulator();

        _background = new BackgroundDisplay(range);
        _tilemap = new TilemapDisplay(range, populator);
    }

    public void Set(Vec2Int pos, TileData tileData) => _tilemap?.Set(pos, tileData);

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        _background?.RenderSprites(targetSprites, cameraView);
        _tilemap?.RenderSprites(targetSprites, cameraView);
    }
}
