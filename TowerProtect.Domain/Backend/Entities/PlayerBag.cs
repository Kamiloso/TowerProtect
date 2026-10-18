using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Inventions.Networking.Packets.Requests;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Entities;

[AutoInterface]
internal class PlayerBag(IEntityBag entityBag) : IPlayerBag
{
    private readonly HashSet<long> _clients = [];

    public event Action<long, Guid>? OnPlayerSpawnedEvent;

    public void OnHello(long client, HelloDto _)
    {
        if (!_clients.Add(client))
            return;

        PlayerEntity player = new(Vec2.Zero);
        entityBag.SpawnEntity(player, out var guid);

        OnPlayerSpawnedEvent?.Invoke(client, guid);
    }
}
