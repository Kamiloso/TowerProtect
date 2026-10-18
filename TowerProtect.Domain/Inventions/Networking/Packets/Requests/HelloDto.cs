using TowerProtect.Domain.Inventions.Networking.Packets.Interfaces;

namespace TowerProtect.Domain.Inventions.Networking.Packets.Requests;

public readonly record struct HelloDto : IPacket
{
    public static int Identifier => 1;
}
