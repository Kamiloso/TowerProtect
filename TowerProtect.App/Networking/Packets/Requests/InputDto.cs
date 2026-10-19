namespace TowerProtect.App.Networking.Packets.Requests;

public readonly record struct InputDto(
    double Rotation,
    bool IsMoving,
    bool IsShooting) : IPacket
{
    public static int Identifier => 2;
}
