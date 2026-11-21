using System;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Client.Rendering.Model;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Client.Tiles.Model;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Gameplay.Tiles;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Client.Tiles.Rendering;

internal static class TileRenderingExtensions
{
    public static IRenderable Render(this TileType self, Vec2Int pos, Func<Vec2Int, TileType?> tileAt)
    {
        Contour contour = 0;
        contour |= PartialContour(Vec2Int.Up, Contour.Up);
        contour |= PartialContour(Vec2Int.Down, Contour.Down);
        contour |= PartialContour(Vec2Int.Left, Contour.Left);
        contour |= PartialContour(Vec2Int.Right, Contour.Right);
        contour |= PartialContour(Vec2Int.Right + Vec2Int.Up, Contour.RightUp);
        contour |= PartialContour(Vec2Int.Right + Vec2Int.Down, Contour.RightDown);
        contour |= PartialContour(Vec2Int.Left + Vec2Int.Up, Contour.LeftUp);
        contour |= PartialContour(Vec2Int.Left + Vec2Int.Down, Contour.LeftDown);

        if (contour == 0)
        {
            return self.IsInvisible()
                ? NoSprites.Instance
                : self.MainSprite(pos);
        }

        Sprite contourSprite = new(
            textureName: $"#c:{contour.ToKey()}",
            position: pos,
            displayLayer: self.GetDisplayLayer().SubLayer(1)
        );

        return self.IsInvisible()
            ? new SpriteGroup(pos, [contourSprite])
            : new SpriteGroup(pos, [self.MainSprite(pos), contourSprite]);

        Contour PartialContour(Vec2Int delta, Contour target)
        {
            int contourLevel = self.GetContourLevel();
            int? otherContourLevel = tileAt(pos + delta)?.GetContourLevel();

            if (!otherContourLevel.HasValue)
                return 0;

            return contourLevel > otherContourLevel.Value ? target : 0;
        }
    }

    private static Sprite MainSprite(this TileType self, Vec2 position) => new(
        textureName: self.GetTextureName(),
        position: position,
        displayLayer: self.GetDisplayLayer()
    );
}
