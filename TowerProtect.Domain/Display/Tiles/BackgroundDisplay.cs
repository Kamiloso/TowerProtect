using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Inventions.Rendering.Sprites;
using TowerProtect.Domain.Inventions.Rendering.Model;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.Domain.Display.Tiles;

internal class BackgroundDisplay(int range) : ISpriteRenderingAware
{
    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        cameraView.ExtendedVisionArea.GridIterate(pos =>
        {
            bool isBelowTilemap = Area.ContainsKey(range, pos);

            Sprite bgSprite = isBelowTilemap
                ? new(pos, Assets.Floor, Layer.Floor)
                : new(pos, Assets.Border, Layer.Border);

            targetSprites.Add(bgSprite);
        });
    }
}
