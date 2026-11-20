using AutoInterfaceAttributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.Framework.Rendering.Textures;

[AutoInterface]
internal class TextureComponent(
    Game game,
    IContourTextureComponent contourTextureComponent,
    IRepeatTextureComponent repeatTextureComponent,
    IUpscaleTextureComponent upscaleTextureComponent) : GameComponent(game), ITextureComponent
{
    private LazyProvider<string, Texture2D> _textures = null!;

    public override void Initialize()
    {
        _textures = new LazyProvider<string, Texture2D>(AllocateTexture);
        base.Initialize();
    }

    public Texture2D Get(string fullKey) => _textures[fullKey];

    private Texture2D AllocateTexture(string fullKey)
    {
        if (!fullKey.Contains('|'))
            return AllocateBaseTexture(fullKey);

        string[] parts = fullKey.Split('|');

        Texture2D baseTexture = Get(parts[0]);
        Texture2D current = baseTexture;

        for (int i = 1; i < parts.Length; i++)
        {
            Texture2D next = ApplyModifier(current, parts[i]);

            if (!ReferenceEquals(current, baseTexture) && !ReferenceEquals(current, next))
                current.Dispose();

            current = next;
        }

        return current;
    }

    private Texture2D AllocateBaseTexture(string baseKey)
    {
        bool isGenerated = baseKey.StartsWith('#');

        Texture2D rawTexture = isGenerated
            ? GenerateTexture(baseKey)
            : Game.Content.Load<Texture2D>(baseKey);

        Texture2D upscaled = upscaleTextureComponent.Upscale(rawTexture);

        if (isGenerated && !ReferenceEquals(rawTexture, upscaled))
            rawTexture.Dispose();

        return upscaled;
    }

    private Texture2D GenerateTexture(string key)
    {
        if (key.StartsWith("#c:", StringComparison.Ordinal))
            return contourTextureComponent.CreateByKey(key[3..]);

        throw new ContentLoadException($"Invalid texture generator: {key}");
    }

    private Texture2D ApplyModifier(Texture2D source, string modifierToken)
    {
        string[] parts = modifierToken.Split(':', 2);

        if (parts.Length != 2)
            throw new ContentLoadException($"Invalid modifier format: {modifierToken}");

        string code = parts[0];
        string args = parts[1];

        if (code == "r")
            return repeatTextureComponent.Apply(source, args);

        throw new ContentLoadException($"Unknown texture modifier: {code}");
    }
}
