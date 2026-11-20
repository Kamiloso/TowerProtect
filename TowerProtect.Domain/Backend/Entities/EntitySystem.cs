using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Abstract.Dynamic;
using TowerProtect.Domain.Backend.Physics;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities;

partial interface IEntitySystem : IUpdateAware
{
    internal void SpawnEntity(Entity entity, out Guid guid);
}

[AutoInterface]
internal class EntitySystem(
    IPhysicsSystem physicsSystem,
    EntityConfig cfg) : IEntitySystem
{
    private readonly Dictionary<Guid, Entity> _entities = [];

    public event Action<Vec2, EntityType>? OnSummonEvent;
    public event Action<Vec2, double>? OnShootEvent;

    private IEnumerable<Entity> EntitiesView => _entities.Values;
    private IEnumerable<Entity> EntitiesSnapshotWhereAlive => EntitiesView.ToList()
        .Where(e => e.IsAlive);

    private IEnumerable<Vec2> RootPositionsView => EntitiesView
        .Where(e => e.IsRoot)
        .Select(e => e.Position);

    public void Update(double deltaTime)
    {
        List<Vec2> rootPositions = [.. RootPositionsView];

        foreach (var entity in EntitiesSnapshotWhereAlive)
        {
            if (InUpdateRange(entity, rootPositions))
            {
                var aware = entity as IUpdateAware;
                aware?.Update(deltaTime);
            }
        }
    }

    [IgnoreAutoInterface]
    public void SpawnEntity(Entity entity, out Guid guid)
    {
        entity.OnDeathEvent += () => DetachEntity(entity);

        entity.OnSummonEvent +=
            (position, entityType) => OnSummonEvent?.Invoke(position, entityType);

        entity.OnShootEvent +=
            (position, rotation) => OnShootEvent?.Invoke(position, rotation);

        if (entity is DynamicEntity dynamicEntity)
        {
            var collider = dynamicEntity.Collider;
            physicsSystem.AttachDynamicCollider(collider);
        }

        _entities.Add(entity.Guid, entity);

        guid = entity.Guid;
    }

    private void DetachEntity(Entity entity)
    {
        if (entity is DynamicEntity dynamicEntity)
        {
            var collider = dynamicEntity.Collider;
            physicsSystem.DetachDynamicCollider(collider);
        }

        _entities.Remove(entity.Guid);
    }

    public EntityListData CollectEntityListData()
    {
        List<Vec2> rootPositions = [.. RootPositionsView];

        List<EntityData> entityDataList = [.. EntitiesView
            .Where(e => e.IsRendered && InUpdateRange(e, rootPositions))
            .Select(e => e.EntityData)];

        return new([.. entityDataList]);
    }

    private bool InUpdateRange(Entity entity, List<Vec2> rootPositions)
    {
        foreach (var rootPos in rootPositions)
        {
            if (Vec2.Distance(entity.Position, rootPos) < cfg.UpdateRange)
                return true;
        }

        return false;
    }
}
