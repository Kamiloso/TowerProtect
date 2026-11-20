using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Systems;

[AutoInterface(Modifier = "internal")]
internal class EntityProvider(
    DifficultyConfig cfg,
    Random rng) : IEntityProvider
{
    public Entity GetPlayer(IEntityController controller)
        => new PlayerEntity(Vec2.Zero, controller);

    public Entity GetProjectile(Vec2 position, double rotation)
        => new ProjectileEntity(position, rotation);

    public Entity GetEnemy(Vec2 position, EntityType enemyType)
    {
        return enemyType switch
        {
            EntityType.Animal => new AnimalEntity(position, new EnemyController()),
            EntityType.Monster => new MonsterEntity(position, new EnemyController()),
            EntityType.Fruit => new MonsterEntity(position, new EnemyController()), // TEMP
            // EntityType.Insect => new InsectEntity(position, controller),
            // EntityType.Sniper => new SniperEntity(position, controller),
            // EntityType.Operator => new OperatorEntity(position, controller),
            // EntityType.Drone => new DroneEntity(position, controller),
            _ => throw new NotSupportedException($"Not supported enemy spawn: {enemyType}")
        };
    }

    public IEnumerable<Entity> GetEnemyBurst(Func<Vec2> getPosition, Difficulty difficulty)
    {
        return [.. difficulty.GetEnemyBurst(cfg.Difficulties, rng)
            .Select(enemyType => GetEnemy(getPosition(), enemyType))];
    }
}
