using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Systems;

partial interface IEntitySystem : IUpdateAware
{
    internal void SpawnEntity(Entity entity, out Guid guid);
}

[AutoInterface]
internal class EntitySystem : IEntitySystem
{
    private readonly Dictionary<Guid, Entity> _entities = [];

    public event Action<Vec2, EntityType>? OnSummonEvent;
    public event Action<Vec2, double>? OnShootEvent;

    private IEnumerable<Entity> EntitiesView => _entities.Values;
    private IEnumerable<Entity> EntitiesSnapshotWhereAlive => EntitiesView.ToList()
        .Where(e => e.IsAlive);

    public void Update(double deltaTime)
    {
        foreach (var entity in EntitiesSnapshotWhereAlive)
        {
            var aware = entity as IUpdateAware;
            aware?.Update(deltaTime);
        }
    }

    [IgnoreAutoInterface]
    public void SpawnEntity(Entity entity, out Guid guid)
    {
        entity.OnDeathEvent +=
            () => _entities.Remove(entity.Guid);

        entity.OnSummonEvent +=
            (position, entityType) => OnSummonEvent?.Invoke(position, entityType);

        entity.OnShootEvent +=
            (position, rotation) => OnShootEvent?.Invoke(position, rotation);

        _entities.Add(entity.Guid, entity);
        guid = entity.Guid;
    }

    public EntityListData CollectEntityListData()
    {
        List<EntityData> entityDataList = [.. EntitiesView
            .Where(e => e.IsRendered)
            .Select(e => e.EntityData)];

        return new([.. entityDataList]);
    }
}
