using AutoInterfaceAttributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static TowerProtect.Framework.Rendering.Textures.TextureConst;

namespace TowerProtect.Framework.Rendering.Textures;

[AutoInterface]
internal class UpscaleTextureComponent(Game game) : GameComponent(game), IUpscaleTextureComponent
{
    public Texture2D Upscale(Texture2D baseTexture)
    {
        int baseWidth = baseTexture.Width;
        int baseHeight = baseTexture.Height;

        int scaleX = (MinTextureSize + baseWidth - 1) / baseWidth;
        int scaleY = (MinTextureSize + baseHeight - 1) / baseHeight;
        int scale = Math.Max(1, Math.Max(scaleX, scaleY));

        if (scale == 1)
        {
            return baseTexture;
        }

        int targetWidth = baseWidth * scale;
        int targetHeight = baseHeight * scale;

        Color[] baseData = new Color[baseWidth * baseHeight];
        baseTexture.GetData(baseData);

        Color[] targetData = new Color[targetWidth * targetHeight];

        for (int y = 0; y < targetHeight; y++)
        {
            int sourceRowOffset = (y / scale) * baseWidth;
            int targetRowOffset = y * targetWidth;

            for (int x = 0; x < targetWidth; x++)
            {
                targetData[targetRowOffset + x] = baseData[sourceRowOffset + (x / scale)];
            }
        }

        Texture2D upscaledTexture = new(Game.GraphicsDevice, targetWidth, targetHeight);
        upscaledTexture.SetData(targetData);

        return upscaledTexture;
    }
}
