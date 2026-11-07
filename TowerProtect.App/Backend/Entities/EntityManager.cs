using AutoInterfaceAttributes;
using TowerProtect.Domain.Backend.Entities.Systems;
using TowerProtect.Domain.Backend.Gameplay;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.App.Backend.Entities;

partial interface IEntityManager : IUpdateAware;

[AutoInterface]
internal class EntityManager(
    ISpawnSystem spawnSystem,
    IProgressSystem progressSystem) : IEntityManager
{
    private double? _timeToSpawn;

    public void Update(double deltaTime)
    {
        _timeToSpawn ??= progressSystem.NextWaitingTime();
        _timeToSpawn -= deltaTime;

        if (_timeToSpawn <= 0)
        {
            var difficulty = progressSystem.Difficulty;

            spawnSystem.SpawnEnemyBurst(() => Vec2.Zero, difficulty);
            _timeToSpawn = progressSystem.NextWaitingTime();
        }
    }
}
