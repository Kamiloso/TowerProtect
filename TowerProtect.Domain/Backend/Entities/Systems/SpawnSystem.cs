using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Backend.Entities.Abstract;
using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Backend.Entities.Utils;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Gameplay;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities.Systems;

partial interface ISpawnSystem : IStartAware;

[AutoInterface]
internal class SpawnSystem(
	IEntitySystem entitySystem,
	IEntityProvider entityProvider) : ISpawnSystem
{
	public void Start()
	{
		entitySystem.OnSummonEvent += (a, b) => SpawnEnemy(a, b);
		entitySystem.OnShootEvent += (a, b) => SpawnProjectile(a, b);
	}

	public Guid SpawnPlayer(IEntityController controller) =>
		Spawn(entityProvider.GetPlayer(controller));

	public Guid SpawnProjectile(Vec2 position, double rotation) =>
		Spawn(entityProvider.GetProjectile(position, rotation));

	public Guid SpawnEnemy(Vec2 position, EntityType entityType) =>
		Spawn(entityProvider.GetEnemy(position, entityType));

	public IEnumerable<Guid> SpawnEnemyBurst(Func<Vec2> getPosition, Difficulty difficulty) =>
		[.. entityProvider.GetEnemyBurst(getPosition, difficulty).Select(Spawn)];

	private Guid Spawn(Entity entity)
	{
		entitySystem.SpawnEntity(entity, out var guid);
		return guid;
	}
}
