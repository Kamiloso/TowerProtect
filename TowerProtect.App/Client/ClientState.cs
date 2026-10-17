using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.Domain.Display.Tiles;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Model.Dto;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.App.Client;

partial interface IClientState : ISpriteRenderingAware;

[AutoInterface]
internal class ClientState : IClientState
{
    public WorldDisplay WorldDisplay { get; } = new();

    private bool _initialized;

    public void Initialize(InitializeDto initializeDto)
    {
        if (_initialized) return;
        _initialized = true;

        WorldDisplay.Initialize(initializeDto.Tilemap);
    }

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        WorldDisplay.RenderSprites(targetSprites, cameraView);
    }
}
