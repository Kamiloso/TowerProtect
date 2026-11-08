using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Extensions;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;
using TowerProtect.Domain.Rendering.Camera;
using TowerProtect.Domain.Rendering.Model;
using TowerProtect.Domain.Rendering.Sprites;

namespace TowerProtect.Domain.Client.Tiles;

internal class BackgroundDisplay(int range) : ISpriteRenderingAware
{
    private const int K = 11;
    private const int R = 5;

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        cameraView.ExtendedVisionArea(K).GridIterate(pos =>
        {
            if (!TryGetAxisInfo(pos.x, out bool isPrimaryX) ||
                !TryGetAxisInfo(pos.y, out bool isPrimaryY))
                return;

            bool isPrimaryAlignment = isPrimaryX && isPrimaryY;
            bool isBelowTilemap = isPrimaryAlignment && pos.MaxAbs <= range + R;

            var asset = isBelowTilemap ? Assets.Floor : Assets.Border;
            var rect = new Rect(pos, K * Vec2Int.One);
            var layer = Layer.Floor + (isPrimaryAlignment ? 0 : 1);

            targetSprites.Add(new($"#r:{asset},{K},{K}", rect, layer));
        });
    }

    private bool TryGetAxisInfo(int p, out bool isPrimary)
    {
        isPrimary = p <= range + R && (p + range).ProperModulo(K) == R;
        return isPrimary || (p > range && (p - range - 1).ProperModulo(K) == R);
    }
}
