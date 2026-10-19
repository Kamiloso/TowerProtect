using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.Domain.Backend.Entities;

public partial interface IEntityBag : IUpdateAware;

[AutoInterface]
public class EntityBag : IEntityBag
{
    private readonly Dictionary<Guid, Entity> _entities = [];

    private IEnumerable<Entity> EntitiesView => _entities.Values;
    private IEnumerable<Entity> EntitiesSnapshotSafe => EntitiesView.ToList()
        .Where(e => e.IsAlive);

    public void Update(double deltaTime)
    {
        foreach (var entity in EntitiesSnapshotSafe)
        {
            var updateAware = entity as IUpdateAware;
            updateAware?.Update(deltaTime);
        }
    }

    public void SpawnEntity(Entity entity, out Guid guid)
    {
        entity.OnDeathEvent += () =>
        {
            _entities.Remove(entity.Guid);
        };

        _entities.Add(entity.Guid, entity);
        guid = entity.Guid;
    }

    public EntityListData CollectEntityListData()
        => new([.. EntitiesView.Select(e => e.EntityData)]);
}
