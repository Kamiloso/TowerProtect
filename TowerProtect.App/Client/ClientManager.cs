using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Dto;
using TowerProtect.Domain.Model.Tiles;
using TowerProtect.App.Engine;
using TowerProtect.Domain.Display.Camera;
using TowerProtect.Domain.Model.Vectors;
using System.Collections.Generic;
using TowerProtect.Domain.Backend.Maps;
using TowerProtect.Domain.Backend.Maps.Specific;

namespace TowerProtect.App.Client;

partial interface IClientManager : IStartAware, IUpdateAware;

[AutoInterface]
internal class ClientManager(
    IClientState clientState,
    IInputSystem inputSystem,
    ICameraZoomControl cameraZoomControl,
    ICameraFollow cameraFollow) : IClientManager
{
    public void Start()
    {
        Map map = new PlaceholderMap();

        int range = map.Range;

        List<PosTileData> posTileList = [];
        for (int x = -range; x <= range; x++)
            for (int y = -range; y <= range; y++)
            {
                var pos = new Vec2Int(x, y);
                var posTileData = map.CreateTileAt(pos).GetPosTileData(pos);

                posTileList.Add(posTileData);
            }

        InitializeDto initializeDto = new(
            Guid.NewGuid(),
            new TilemapData(range, [.. posTileList])
        );

        clientState.Initialize(initializeDto);
    }

    public void Update(double deltaTime)
    {
        cameraZoomControl.Update(deltaTime);
        cameraFollow.Update(deltaTime);

        // TODO: move this somewhere else (follow player)

        if (inputSystem.IsKeyDown("R"))
        {
            cameraFollow.Enabled = !cameraFollow.Enabled;
        }

        if (inputSystem.ArrowsVector != Vec2.Zero)
        {
            cameraFollow.Target += inputSystem.ArrowsVector;
        }
    }
}
