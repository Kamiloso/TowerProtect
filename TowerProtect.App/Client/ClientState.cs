using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.Domain.Display.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Networking.Packets.Responses;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Inventions.Rendering.Sprites;

namespace TowerProtect.App.Client;

partial interface IClientState : ISpriteRenderingAware;

[AutoInterface]
internal class ClientState(IWorldDisplay worldDisplay) : IClientState
{
    public void Initialize(InitializeDto initializeDto)
    {
        worldDisplay.Initialize(initializeDto.Tilemap);
    }

    public void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView)
    {
        worldDisplay.RenderSprites(targetSprites, cameraView);
    }
}
