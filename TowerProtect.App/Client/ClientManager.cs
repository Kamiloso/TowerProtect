using AutoInterfaceAttributes;
using TowerProtect.Domain.Display.Camera;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.App.Networking.Client;
using TowerProtect.Domain.Inventions.Networking.Packets.Requests;
using TowerProtect.Domain.Inventions.Networking.Packets.Responses;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.App.Client.Technology;

namespace TowerProtect.App.Client;

partial interface IClientManager : IStartAware, IUpdateAware;

[AutoInterface]
internal class ClientManager(
    IClientRouter clientRouter,
    ICameraZoomControl cameraZoomControl,
    ICameraFollow cameraFollow,
    IClientState clientState,
    IInputSystem inputSystem) : IClientManager
{
    public void Start()
    {
        clientRouter.Subscribe<InitializeDto>(clientState.Initialize);
        clientRouter.Send<HelloDto>(new());
    }

    public void Update(double deltaTime)
    {
        clientRouter.Update(deltaTime);
        cameraZoomControl.Update(deltaTime);
        cameraFollow.Update(deltaTime);

        // TODO: move this somewhere else (follow player)
        if (inputSystem.ArrowsVector != Vec2.Zero)
        {
            cameraFollow.Enabled = true;
            cameraFollow.Target += inputSystem.ArrowsVector;
        }
    }
}
