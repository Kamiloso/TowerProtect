using AutoInterfaceAttributes;
using TowerProtect.App.Networking.Client;
using TowerProtect.App.Networking.Packets.Requests;
using TowerProtect.App.Networking.Packets.Responses;
using TowerProtect.Domain.Display.Entities;
using TowerProtect.Domain.Display.Tiles;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Client;

partial interface IClientStateManager : IStartAware;

// TODO: split this class into two:
// - one for initialization (InitializeDto)
// - one for updates (BroadcastDto)

// Additionally add a proper "ClientStateManager" logic

[AutoInterface]
internal class ClientStateManager(
    IWorldDisplay worldDisplay,
    IEntitiesDisplay entitiesDisplay,
    IClientRouter clientRouter) : IClientStateManager
{
    public void Start()
    {
        clientRouter.Subscribe<InitializeDto>(OnInitialize);
        clientRouter.Send(new HelloDto());

        clientRouter.Subscribe<BroadcastDto>(OnBroadcast);
    }

    private void OnInitialize(InitializeDto initializeDto)
    {
        worldDisplay.PushInformation(initializeDto.Tilemap);
        entitiesDisplay.PushInformation(initializeDto.PlayerGuid);
    }

    private void OnBroadcast(BroadcastDto broadcastDto)
    {
        entitiesDisplay.NextUpdate(broadcastDto.EntityList);
    }
}
