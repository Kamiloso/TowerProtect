using System;
using TowerProtect.Domain.Inventions.Networking.Packets.Interfaces;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Inventions.Networking.Packets.Responses;

public readonly record struct InitializeDto(
    Guid PlayerGuid,
    TilemapData Tilemap) : IPacket
{
    public static int Identifier => -1;
}
