using System.Collections.Generic;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Rendering;
using TowerProtect.Domain.Rendering.Model;
using TowerProtect.Domain.Tools;

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
