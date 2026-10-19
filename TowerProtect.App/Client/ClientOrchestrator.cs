using AutoInterfaceAttributes;
using TowerProtect.Domain.Display.Camera;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.App.Networking.Client;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.App.Client.Input;

namespace TowerProtect.App.Client;

partial interface IClientOrchestrator : IStartAware, IUpdateAware;

[AutoInterface]
internal class ClientOrchestrator(
    IClientRouter clientRouter,
    IClientStateManager clientStateManager,
    IInputSender inputSender,
    ICameraZoomControl cameraZoomControl,
    ICameraFollow cameraFollow,


    // TODO: move this
    IInputSystem inputSystem) : IClientOrchestrator
{
    public void Start()
    {
        clientStateManager.Start();
    }

    public void Update(double deltaTime)
    {
        clientRouter.Update(deltaTime);
        inputSender.Update(deltaTime);
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
