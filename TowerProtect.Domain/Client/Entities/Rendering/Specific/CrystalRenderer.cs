using System;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Client.Entities.Rendering.Specific;

internal sealed class CrystalRenderer(EntityType type) : SimpleRenderer(type)
{
    protected override Animated<IRenderable> Animate(Guid guid, bool isMain)
        => new(t =>
        {
            int hash1 = guid.GetHashCode(), hash2;
            unchecked { hash2 = hash1 + 1; }

            double rotation = t * Type.GetCrystalRotationSpeed();

            var body1 = Body(isMain, hash1).Rotate(rotation);
            var body2 = Body(isMain, hash2).Rotate(rotation + Math.PI / 4.0);

            return new SpriteGroup([body1, body2]);
        });
}
