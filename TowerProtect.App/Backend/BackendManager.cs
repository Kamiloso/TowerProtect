using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using TowerProtect.App.Networking.Backend;
using TowerProtect.Domain.Backend.Entities;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Inventions.Networking.Packets.Requests;
using TowerProtect.Domain.Inventions.Networking.Packets.Responses;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.App.Backend;

partial interface IBackendManager : IStartAware, IUpdateAware;

[AutoInterface]
internal class BackendManager(
    IBackendRouter backendRouter,
    ITilemap tilemap,
    IEntityBag entityBag) : IBackendManager
{
    public void Start()
    {
        backendRouter.Subscribe<HelloDto>(OnHello);
    }

    public void Update(double deltaTime)
    {
        backendRouter.Update(deltaTime);
        entityBag.Update(deltaTime);
    }

    // TODO: move what is below to separate domain class

    private readonly HashSet<long> _clients = [];

    public void OnHello(long client, HelloDto helloDto)
    {
        if (!SpawnPlayer(client, out var guid))
            return;

        backendRouter.Send(client, new InitializeDto(
            guid, tilemap.CollectTilemapData()
        ));
    }

    private bool SpawnPlayer(long client, out Guid guid)
    {
        if (!_clients.Add(client))
        {
            guid = default;
            return false;
        }

        var player = new PlayerEntity(Vec2.Zero);
        entityBag.SpawnEntity(player, out guid);

        return true;
    }
}
