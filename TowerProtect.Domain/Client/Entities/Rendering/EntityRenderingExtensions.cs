using System;
using System.Diagnostics;
using TowerProtect.Domain.Client.Entities.Rendering.Abstract;
using TowerProtect.Domain.Client.Rendering;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Client.Entities.Rendering;

internal static class EntityRenderingExtensions
{
    public static IRenderable Render(this EntityData entity, Guid mainGuid, Stopwatch sw)
    {
        var (guid, entityType, position, rotation) = entity;

        bool isMain = guid == mainGuid;

        return EntityRenderer.Of(entityType)
            .Render(position, rotation, guid, isMain, sw);
    }
}
