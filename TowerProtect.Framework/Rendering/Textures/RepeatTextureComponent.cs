using AutoInterfaceAttributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerProtect.Framework.Rendering.Textures;

[AutoInterface]
internal class RepeatTextureComponent(Game game) : GameComponent(game), IRepeatTextureComponent
{
    public Texture2D Apply(Texture2D source, string args)
    {
        string[] parts = args.Split(',');

        if (parts.Length != 2)
            throw new ArgumentException($"Invalid repeat arguments: {args} (expected: rx,ry)");

        int repeatX = Math.Max(int.Parse(parts[0]), 1);
        int repeatY = Math.Max(int.Parse(parts[1]), 1);

        if (repeatX == 1 && repeatY == 1)
            return source;

        return Repeat(source, repeatX, repeatY);
    }

    private Texture2D Repeat(Texture2D baseTexture, int repeatX, int repeatY)
    {
        int baseWidth = baseTexture.Width;
        int baseHeight = baseTexture.Height;
        int targetWidth = baseWidth * repeatX;
        int targetHeight = baseHeight * repeatY;

        Color[] baseColorData = new Color[baseWidth * baseHeight];
        baseTexture.GetData(baseColorData);

        Color[] targetColorData = new Color[targetWidth * targetHeight];

        for (int y = 0; y < targetHeight; y++)
        {
            int sourceRowOffset = (y % baseHeight) * baseWidth;
            int targetRowOffset = y * targetWidth;

            for (int x = 0; x < targetWidth; x++)
            {
                targetColorData[targetRowOffset + x] = baseColorData[sourceRowOffset + (x % baseWidth)];
            }
        }

        Texture2D repeatedTexture = new(Game.GraphicsDevice, targetWidth, targetHeight);
        repeatedTexture.SetData(targetColorData);

        return repeatedTexture;
    }
}
