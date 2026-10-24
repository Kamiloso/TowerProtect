using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TowerProtect.Domain.Client.Entities.Extensions;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Inventions.Rendering.Sprites;
using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.Domain.Client.Entities;

partial interface IEntitiesDisplay : ISpriteRenderingAware;

[AutoInterface]
internal class EntitiesDisplay : IEntitiesDisplay
{
    private readonly Dictionary<Guid, EntityData> _entities = [];
    private readonly Stopwatch _sw = Stopwatch.StartNew();

    private Guid _mainGuid;

    public void PushInformation(Guid mainGuid)
    {
        _mainGuid = mainGuid;
    }

    public void NextUpdate(EntityListData entityList)
    {
        _entities.Clear();

        foreach (var entity in entityList.Unpack())
        {
            _entities.Add(entity.Guid, entity);
        }
    }

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        foreach (var entity in _entities.Values)
        {
            var sprites = entity.Render(_mainGuid, _sw).Sprites;
            targetSprites.AddRange(sprites);
        }
    }
}
