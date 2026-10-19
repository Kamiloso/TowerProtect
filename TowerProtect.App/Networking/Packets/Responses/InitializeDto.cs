using System;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.App.Networking.Packets.Responses;

public readonly record struct InitializeDto(
    Guid PlayerGuid,
    TilemapData Tilemap) : IPacket
{
    public static int Identifier => -2;
}
