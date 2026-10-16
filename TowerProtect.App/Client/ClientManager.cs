using AutoInterfaceAttributes;
using System;
using TowerProtect.App.Engine;
using TowerProtect.Domain.Game.Tiles.Model;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Client;

partial interface IClientManager : IUpdateAware;

[AutoInterface]
internal class ClientManager : IClientManager
{
    private readonly IClientState _clientState;
    private readonly IInputSystem _inputSystem;
    private readonly ICameraView _cameraView;

    public ClientManager(IClientState clientState, IInputSystem inputSystem, ICameraView cameraView)
    {
        _clientState = clientState;
        _inputSystem = inputSystem;
        _cameraView = cameraView;

        _clientState.Start(range: 100);

        for (int i = -100; i <= 100; i++)
            for (int j = -100; j <= 100; j++)
            {
                if (Random.Shared.Next(5) == 0)
                {
                    Vec2Int pos = new(i, j);
                    _clientState.Tilemap.Set(pos, new TileData(TileType.Stone, 1));
                }
            }
    }

    public void Update(double deltaTime)
    {
        _cameraView.Center += _inputSystem.ArrowsVector;
        _cameraView.Zoom += 0.15 * _inputSystem.ScrollSign;
    }
}
