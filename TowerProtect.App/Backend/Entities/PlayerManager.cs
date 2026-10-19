using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.App.Networking.Backend;
using TowerProtect.App.Networking.Packets.Requests;
using TowerProtect.App.Networking.Packets.Responses;
using TowerProtect.Domain.Backend.Entities;
using TowerProtect.Domain.Backend.Entities.Movement;
using TowerProtect.Domain.Backend.Entities.Specific;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.App.Backend;

partial interface IPlayerManager : IStartAware;

[AutoInterface]
internal class PlayerManager(
    IBackendRouter backendRouter,
    IEntityBag entityBag,
    ITilemap tilemap) : IPlayerManager
{
    private readonly Dictionary<long, ManualController> _controllers = [];

    public void Start()
    {
        backendRouter.Subscribe<HelloDto>(OnHello);
        backendRouter.Subscribe<InputDto>(OnInput);
    }

    public void OnHello(long client, HelloDto helloDto)
    {
        if (_controllers.ContainsKey(client))
            return;

        ManualController controller = new();
        _controllers.Add(client, controller);

        PlayerEntity player = new(Vec2.Zero, controller);
        entityBag.SpawnEntity(player, out var guid);

        backendRouter.Send(client, new InitializeDto(
            PlayerGuid: guid,
            Tilemap: tilemap.CollectTilemapData()
        ));
    }

    public void OnInput(long client, InputDto inputDto)
    {
        if (!_controllers.TryGetValue(client, out var controller))
            return;

        var (rotation, isMoving, isShooting) = inputDto;

        controller.Update(rotation, isMoving, isShooting);
    }
}
