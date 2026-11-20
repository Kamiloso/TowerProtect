using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.App.Backend.Entities;
using TowerProtect.App.Networking.Backend;
using TowerProtect.Domain.Backend.Entities.Systems;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Backend;

partial interface IBackendOrchestrator : IAwareChain;

[AutoInterface]
internal class BackendOrchestrator(
    IBackendRouter backendRouter,
    ITilemap tilemap,
	IEntityManager entityManager,
    IPlayerManager playerManager,
	IEntitySystem entitySystem,
    ISpawnSystem spawnSystem,
    IBroadcastSender broadcastSender) : IBackendOrchestrator
{
    IEnumerable<object> IAwareChain.Children => [
        backendRouter,
        tilemap,
        entityManager,
        playerManager,
        entitySystem,
        spawnSystem,
        broadcastSender,
    ];
}
