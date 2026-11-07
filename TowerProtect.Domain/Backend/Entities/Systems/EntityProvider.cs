using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Gameplay;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Utils;

partial interface IEntityProvider
{
    internal Entity GetPlayer(IEntityController controller);
    internal Entity GetProjectile(Vec2 position, double rotation);
    internal Entity GetEnemy(Vec2 position, EntityType enemyType);
    internal IEnumerable<Entity> GetEnemyBurst(Func<Vec2> getPosition, Difficulty difficulty);
}

[AutoInterface]
internal class EntityProvider(Random rng) : IEntityProvider
{
    [IgnoreAutoInterface]
    public Entity GetPlayer(IEntityController controller)
        => new PlayerEntity(Vec2.Zero, controller);

    [IgnoreAutoInterface]
    public Entity GetProjectile(Vec2 position, double rotation)
        => new ProjectileEntity(position, rotation);

    [IgnoreAutoInterface]
    public Entity GetEnemy(Vec2 position, EntityType enemyType)
    {
        return enemyType switch
        {
            EntityType.Animal => new AnimalEntity(position, new EnemyController()),
            EntityType.Monster => new MonsterEntity(position, new EnemyController()),
            // EntityType.Insect => new InsectEntity(position, controller),
            // EntityType.Sniper => new SniperEntity(position, controller),
            // EntityType.Operator => new OperatorEntity(position, controller),
            // EntityType.Drone => new DroneEntity(position, controller),
            _ => throw new NotSupportedException($"Not supported enemy spawn: {enemyType}")
        };
    }

    [IgnoreAutoInterface]
    public IEnumerable<Entity> GetEnemyBurst(Func<Vec2> getPosition, Difficulty difficulty)
    {
        return [.. difficulty.GetEnemyBurst(rng)
            .Select(enemyType => GetEnemy(getPosition(), enemyType))];
    }
}
