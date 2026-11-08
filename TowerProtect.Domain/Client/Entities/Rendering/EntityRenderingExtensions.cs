using System;
using System.Diagnostics;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Rendering;
using TowerProtect.Domain.Rendering.Model;
using TowerProtect.Domain.Rendering.Sprites;

namespace TowerProtect.Domain.Client.Entities.Rendering;

internal static class EntityRenderingExtensions
{
    public static IRenderable Render(this EntityData entity, Guid mainGuid, Stopwatch sw)
    {
        var (guid, entityType, position, rotation) = entity;

        var layer = guid == mainGuid
            ? Layer.Player
            : Layer.Entity;

        Animated<IRenderable> animated = RenderEmpty();

        if (entityType.IsSimpleRender())
            animated = RenderSimple(entityType, layer, guid);

        if (entityType is EntityType.Projectile)
            animated = RenderProjectile(layer, guid);

        return animated.Evaluate(sw).Move(position).Rotate(rotation);
    }

    private static Animated<IRenderable> RenderEmpty() => new(_ => NoSprites.Instance);

    private static Animated<IRenderable> RenderSimple(EntityType entityType, Layer layer, Guid guid)
        => new(_ => new Sprite(
            textureName: entityType.GetTextureName(),
            displayLayer: layer,
            sortEntropy: guid.GetHashCode())
        );

    private static Animated<IRenderable> RenderProjectile(Layer layer, Guid guid)
        => new(_ => new Sprite(
            textureName: EntityType.Projectile.GetTextureName(),
            displayLayer: layer,
            sortEntropy: guid.GetHashCode())
        );
}
