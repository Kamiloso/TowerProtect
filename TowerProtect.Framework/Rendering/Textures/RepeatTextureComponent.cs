using AutoInterfaceAttributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerProtect.Framework.Rendering.Textures;

[AutoInterface]
internal class RepeatTextureComponent(Game game) : GameComponent(game), IRepeatTextureComponent
{
    public Texture2D CreateByKey(string key)
    {
        string[] parts = key.Split(',');

        if (parts.Length != 3)
            throw new ArgumentException($"Invalid repeat texture format: {key} (expected: name,rx,ry)");

        string baseTextureName = parts[0];
        int repeatX = int.Parse(parts[1]);
        int repeatY = int.Parse(parts[2]);

        Texture2D baseTexture = Game.Content.Load<Texture2D>(baseTextureName);

        int baseWidth = baseTexture.Width;
        int baseHeight = baseTexture.Height;
        int targetWidth = baseWidth * repeatX;
        int targetHeight = baseHeight * repeatY;

        Color[] baseColorData = new Color[baseWidth * baseHeight];
        baseTexture.GetData(baseColorData);

        Color[] targetColorData = new Color[targetWidth * targetHeight];

        for (int y = 0; y < targetHeight; y++)
        {
            for (int x = 0; x < targetWidth; x++)
            {
                int sourceX = x % baseWidth;
                int sourceY = y % baseHeight;
                targetColorData[y * targetWidth + x] = baseColorData[sourceY * baseWidth + sourceX];
            }
        }

        Texture2D repeatedTexture = new Texture2D(Game.GraphicsDevice, targetWidth, targetHeight);
        repeatedTexture.SetData(targetColorData);

        return repeatedTexture;
    }
}
