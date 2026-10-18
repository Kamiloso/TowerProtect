using AutoInterfaceAttributes;
using TowerProtect.App.Networking.Backend;
using TowerProtect.Domain.Backend.Entities;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Networking.Packets.Requests;
using TowerProtect.Domain.Inventions.Networking.Packets.Responses;

namespace TowerProtect.App.Backend;

partial interface IBackendManager : IStartAware, IUpdateAware;

[AutoInterface]
internal class BackendManager(
    IBackendRouter backendRouter,
    ITilemap tilemap,
    IEntityBag entityBag,
    IPlayerBag playerBag) : IBackendManager
{
    public void Start()
    {
        backendRouter.Subscribe<HelloDto>(playerBag.OnHello);
        playerBag.OnPlayerSpawnedEvent += (client, guid) =>
        {
            backendRouter.Send(client, new InitializeDto(
                guid, tilemap.CollectTilemapData())
            );
        };
    }

    public void Update(double deltaTime)
    {
        backendRouter.Update(deltaTime);
        entityBag.Update(deltaTime);
    }
}
