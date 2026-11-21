using System;
using System.Diagnostics;
using TowerProtect.Domain.Client.Entities.Rendering.Specific;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Client.Entities.Rendering.Abstract;

internal abstract class EntityRenderer
{
    public IRenderable Render(Vec2 position, double rotation, Guid guid, bool isMain, Stopwatch sw)
        => Animate(guid, isMain).Evaluate(sw).Move(position).Rotate(rotation);

    protected abstract Animated<IRenderable> Animate(Guid guid, bool isMain);

    public static EntityRenderer Of(EntityType type) => type switch
    {
        _ when type.HasSimpleRenderer() => new SimpleRenderer(type),
        _ when type.HasCrystalRenderer() => new CrystalRenderer(type),
        _ => new EmptyRenderer()
    };
}
