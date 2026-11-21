using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Systems;

[AutoInterface(Modifier = "internal")]
internal class EntityFactory(Random rng) : IEntityFactory
{
    public Entity GetPlayer(IEntityController controller)
        => new PlayerEntity(Vec2.Zero, controller);

    public Entity GetProjectile(Vec2 position, double rotation, EntityType projectileType) => projectileType switch
    {
        EntityType.Projectile => new ProjectileEntity(position, rotation),
        EntityType.EnemyProjectile => new ProjectileEntity(position, rotation), // TEMP
        _ => throw new InvalidOperationException($"Not supported projectile spawn: {projectileType}")
    };

    public Entity GetEnemy(Vec2 position, EntityType enemyType)
    {
        return enemyType switch
        {
            EntityType.Animal => new AnimalEntity(position, new WanderController()),
            EntityType.Monster => new MonsterEntity(position, new WanderController()),
            EntityType.Fruit => new MonsterEntity(position, new WanderController()), // TEMP
            EntityType.MagicNest => new MagicNestEntity(position, new MagicNestController()),
            // EntityType.Insect => new InsectEntity(position, controller),
            // EntityType.Sniper => new SniperEntity(position, controller),
            // EntityType.Operator => new OperatorEntity(position, controller),
            // EntityType.Drone => new DroneEntity(position, controller),
            _ => throw new NotSupportedException($"Not supported enemy spawn: {enemyType}")
        };
    }

    public IEnumerable<Entity> GetEnemyBurst(Func<Vec2> getPosition, DifficultySettings settings)
    {
        return [.. settings.GetEnemyBurst(rng)
            .Select(enemyType => GetEnemy(getPosition(), enemyType))];
    }
}
