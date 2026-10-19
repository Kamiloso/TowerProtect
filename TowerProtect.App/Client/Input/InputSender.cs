using AutoInterfaceAttributes;
using System;
using TowerProtect.App.Networking.Client;
using TowerProtect.App.Networking.Packets.Requests;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using static TowerProtect.Domain.Config.PeriodConfig;

namespace TowerProtect.App.Client.Input;

partial interface IInputSender : IUpdateAware;

[AutoInterface]
internal class InputSender(
    IInputSystem inputSystem,
    IClientRouter clientRouter) : IInputSender
{
    private double _timeAccumulator;

    public void Update(double deltaTime)
    {
        _timeAccumulator += deltaTime;
        if (_timeAccumulator >= InputSendPeriod)
        {
            _timeAccumulator %= deltaTime;

            Vec2 movementVector = (inputSystem.MouseVector + inputSystem.ArrowsVector).Normalized;

            clientRouter.Send(new InputDto(
                Rotation: Math.Atan2(movementVector.y, movementVector.x),
                IsMoving: inputSystem.IsMousePressed(1) || inputSystem.IsKeyPressed("Space"),
                IsShooting: inputSystem.IsMousePressed(0)
            ));
        }
    }
}
