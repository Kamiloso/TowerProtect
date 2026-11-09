using AutoInterfaceAttributes;
using TowerProtect.App.Networking.Backend;
using TowerProtect.App.Networking.Packets.Responses;
using TowerProtect.Domain.Backend.Entities.Systems;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Tools;

namespace TowerProtect.App.Backend.Entities;

partial interface IBroadcastSender : IUpdateAware;

[AutoInterface]
internal class BroadcastSender(
    IEntitySystem entitySystem,
    IBackendRouter backendRouter,
    LoopConfig cfg) : IBroadcastSender
{
    private readonly LoopCycle _broadcastCycle = new(cfg.BroadcastSendPeriod);

    public void Update(double deltaTime)
    {
        _broadcastCycle.StackInvoke(deltaTime, frames =>
        {
            double tickTime = frames * cfg.BroadcastSendPeriod;
            var entityList = entitySystem.CollectEntityListData();

            backendRouter.Send(0, new BroadcastDto(entityList, tickTime));
        });
    }
}
