using AutoInterfaceAttributes;
using System;
using TowerProtect.App.Networking.Backend;
using TowerProtect.App.Networking.Packets.Requests;
using TowerProtect.App.Networking.Packets.Responses;
using TowerProtect.Domain.Backend.Entities.Systems;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Backend;

partial interface IPlayerManager : IStartAware;

[AutoInterface]
internal class PlayerManager(
    IBackendRouter backendRouter,
    IPlayerSystem playerSystem,
    ITilemap tilemap) : IPlayerManager
{
    public void Start()
    {
        backendRouter.Subscribe<HelloDto>(OnHello);
        backendRouter.Subscribe<InputDto>(OnInput);
    }

    public void OnHello(long client, HelloDto helloDto)
    {
        if (!playerSystem.TrySpawn(client, out Guid guid))
            return;

        backendRouter.Send(client, new InitializeDto(
            PlayerGuid: guid,
            Tilemap: tilemap.CollectTilemapData()
        ));
    }

    public void OnInput(long client, InputDto inputDto)
    {
        var inputUnion = inputDto.ToInputUnion();
        playerSystem.AcceptInput(client, inputUnion);
    }
}
