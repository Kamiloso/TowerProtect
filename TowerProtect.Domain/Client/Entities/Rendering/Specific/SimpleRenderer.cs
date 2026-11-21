using System;
using TowerProtect.Domain.Client.Entities.Rendering.Abstract;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Client.Entities.Rendering.Specific;

internal class SimpleRenderer(EntityType type) : EntityRenderer
{
    protected EntityType Type { get; } = type;

    protected override Animated<IRenderable> Animate(Guid guid, bool isMain)
        => new(_ => Body(isMain, guid.GetHashCode()));

    protected Sprite Body(bool isMain, int sortEntropy)
        => new(
            textureName: Type.GetTextureName(),
            side: Type.GetSide(),
            displayLayer: Type.GetDisplayLayer(isMain),
            sortEntropy: sortEntropy
        );
}
