using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.App.Client.Camera;
using TowerProtect.App.Client.Input;
using TowerProtect.App.Client.Receivers;
using TowerProtect.App.Networking.Client;
using TowerProtect.Domain.Client.Entities;
using TowerProtect.Domain.Inventions.Interfaces;

namespace TowerProtect.App.Client;

partial interface IClientOrchestrator : IAwareChain;

[AutoInterface]
internal class ClientOrchestrator(
    IClientRouter clientRouter,
    IInformationDivider informationDivider,
    IInputSender inputSender,
    IEntitiesDisplay entitiesDisplay,
    ICameraControl cameraControl) : IClientOrchestrator
{
    IEnumerable<object> IAwareChain.Children => [
        clientRouter,
        informationDivider,
        inputSender,
        entitiesDisplay,
        cameraControl,
    ];
}
