using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Game.Entities;

public class EntityBag : IUpdateAware
{
    private readonly Dictionary<Guid, Entity> _entities = [];

    private IEnumerable<Entity> EntitiesSnapshot => _entities.Values.ToList()
        .Where(e => e.IsAlive);

    public void Spawn(Entity entity)
    {
        entity.OnDeath += () => _entities.Remove(entity.Guid);
        _entities.Add(entity.Guid, entity);
    }

    public void Update(double deltaTime)
    {
        foreach (var entity in EntitiesSnapshot)
        {
            var updateAware = entity as IUpdateAware;
            updateAware?.Update(deltaTime);
        }
    }

    public void ApplyAreaDamage(Vec2 center, int damage, double radius)
    {
        if (radius <= 0.0) return;

        foreach (var entity in EntitiesSnapshot)
        {
            double distance = Vec2.Distance(center, entity.Position);

            if (distance < radius)
            {
                double ratio = 1.0 - distance / radius;
                int realDamage = (int)Math.Ceiling(ratio * damage);

                entity.ApplyDamage(realDamage);
            }
        }
    }
}
