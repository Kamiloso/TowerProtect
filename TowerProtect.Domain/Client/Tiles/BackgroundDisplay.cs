using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Rendering.Camera;
using TowerProtect.Domain.Rendering.Model;
using TowerProtect.Domain.Rendering.Sprites;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.Domain.Client.Tiles;

internal class BackgroundDisplay(int range) : ISpriteRenderingAware
{
    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        cameraView.ExtendedVisionArea.GridIterate(pos =>
        {
            bool isBelowTilemap = Area.ContainsKey(range, pos);

            Sprite bgSprite = isBelowTilemap
                ? new(Assets.Floor, pos, Layer.Floor)
                : new(Assets.Border, pos, Layer.Border);

            targetSprites.Add(bgSprite);
        });
    }
}
