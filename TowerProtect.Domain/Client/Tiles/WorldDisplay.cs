using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Inventions.Rendering.Sprites;
using TowerProtect.Domain.Model.Tiles;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Display.Tiles;

partial interface IWorldDisplay : ISpriteRenderingAware;

[AutoInterface]
public class WorldDisplay : IWorldDisplay
{
    private const int TypicalMapRange = 24;

    private BackgroundDisplay _background = new(TypicalMapRange);
    private TilemapDisplay? _tilemap;

    public void PushInformation(TilemapData tilemap)
    {
        _background = new BackgroundDisplay(tilemap.Range);
        _tilemap = new TilemapDisplay(tilemap.Range, tilemap.GetPopulator());
    }

    public void Set(Vec2Int pos, TileData tile)
    {
        _tilemap?.Set(pos, tile);
    }

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        _background.RenderSprites(targetSprites, cameraView);
        _tilemap?.RenderSprites(targetSprites, cameraView);
    }
}
