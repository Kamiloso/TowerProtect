using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Control;
using TowerProtect.Domain.Backend.Entities.Systems;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities;

partial interface ISpawnSystem : IStartAware;

[AutoInterface]
internal class SpawnSystem(
	IEntitySystem entitySystem,
	ITilemap tilemap,
	IEntityFactory entityFactory) : ISpawnSystem
{
	public void Start()
	{
		entitySystem.OnSummonEvent +=
			(a, b) => SpawnEnemy(a, b);

		entitySystem.OnShootEvent +=
			(a, b, c) => SpawnProjectile(a, b, c);

		tilemap.OnSummonEvent +=
			(a, b) => SpawnEnemy(a, b);
    }

	public Guid SpawnPlayer(IEntityController controller) =>
		Spawn(entityFactory.GetPlayer(controller));

	public Guid SpawnProjectile(Vec2 position, double rotation, EntityType projectileType) =>
		Spawn(entityFactory.GetProjectile(position, rotation, projectileType));

	public Guid SpawnEnemy(Vec2 position, EntityType entityType) =>
		Spawn(entityFactory.GetEnemy(position, entityType));

	public IEnumerable<Guid> SpawnEnemyBurst(Func<Vec2> getPosition, DifficultySettings settings) =>
		[.. entityFactory.GetEnemyBurst(getPosition, settings).Select(Spawn)];

	private Guid Spawn(Entity entity)
	{
		entitySystem.SpawnEntity(entity, out var guid);
		return guid;
	}
}
