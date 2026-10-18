namespace TowerProtect.Domain.Inventions.Networking.Packets.Interfaces;

public interface IPacket : IHasValidation
{
    static abstract int Identifier { get; }

    // Multiplayer is delayed, so we just ignore validation for now.
    bool IHasValidation.IsValid() => true;
}
