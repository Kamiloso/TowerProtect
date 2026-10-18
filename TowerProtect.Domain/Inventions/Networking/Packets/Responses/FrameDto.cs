using System.Collections.Immutable;
using TowerProtect.Domain.Inventions.Networking.Packets.Interfaces;
using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.Domain.Inventions.Networking.Packets.Responses;

public readonly record struct FrameDto(
    ImmutableArray<EntityData> Entities) : IPacket
{
    public static int Identifier => -2;
}
