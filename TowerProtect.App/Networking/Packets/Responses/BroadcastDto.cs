using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.App.Networking.Packets.Responses;

public readonly record struct BroadcastDto(
    EntityListData EntityList) : IPacket
{
    public static int Identifier => -1;
}
