using AutoInterfaceAttributes;
using TowerProtect.App.Networking.Backend;
using TowerProtect.App.Networking.Packets.Responses;
using TowerProtect.Domain.Backend.Entities;
using TowerProtect.Domain.Inventions.Interfaces;
using static TowerProtect.Domain.Config.PeriodConfig;

namespace TowerProtect.App.Backend.Entities;

partial interface IBroadcastSender : IUpdateAware;

[AutoInterface]
internal class BroadcastSender(
    IEntityBag entityBag,
    IBackendRouter backendRouter) : IBroadcastSender
{
    private double _timeAccumulator;

    public void Update(double deltaTime)
    {
        _timeAccumulator += deltaTime;
        if (_timeAccumulator >= BroadcastSendPeriod)
        {
            _timeAccumulator %= deltaTime;

            var entityList = entityBag.CollectEntityListData();
            backendRouter.Send(0, new BroadcastDto(entityList));
        }
    }
}
