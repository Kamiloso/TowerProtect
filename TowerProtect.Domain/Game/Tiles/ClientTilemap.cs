using System;
using System.Collections.Generic;
using TowerProtect.Domain.Game.Tiles.Model;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;
using TowerProtect.Domain.Tools;

namespace TowerProtect.Domain.Game.Tiles;

public class ClientTilemap : ISpriteRenderingAware
{
    private Area<IRenderable> _renderables = null!;

    public ClientTilemap(int range, Func<Vec2Int, IRenderable>? popualtor = null)
        => Recreate(range, popualtor);

    public void Recreate(int range, Func<Vec2Int, IRenderable>? populator = null)
    {
        populator ??= _ => NoSprites.Instance;

        _renderables = new Area<IRenderable>(range);
        _renderables.Populate(populator);
    }

    public void Set(Vec2Int pos, TileData tileData)
    {
        if (!_renderables.Contains(pos))
            return;

        var tileType = tileData.Type;

        IRenderable renderable = tileType.IsTransparent()
            ? NoSprites.Instance
            : new Sprite(tileType.GetName(), new Rect(pos, Vec2.One));

        _renderables[pos] = renderable;
    }

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        _renderables.Iterate(pos =>
        {
            var renderable = _renderables[pos];
            if (!cameraView.IsCulled(renderable))
            {
                targetSprites.AddRange(renderable.Sprites);
            }
        });
    }
}
