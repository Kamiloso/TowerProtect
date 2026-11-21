using System;
using TowerProtect.Domain.Client.Entities.Rendering.Abstract;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.Domain.Client.Entities.Rendering.Specific;

internal sealed class EmptyRenderer : EntityRenderer
{
    protected override Animated<IRenderable> Animate(Guid guid, bool _)
        => new(_ => NoSprites.Instance);
}
