using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles;

[AutoInterface]
internal class TileFactory(Random rng) : ITileFactory
{
    public Tile CreateTile(TileType tileType) => tileType switch
    {
        TileType.Air => AirTile.Instance,
        TileType.Wall => WallTile.Instance,
        TileType.Spawner => SpawnerTile.Instance,
        TileType.Water => WaterTile.Instance,
        TileType.Bedrock => BedrockTile.Instance,
        TileType.Rock => new RockTile(GetRockDurability()),
        _ => throw new InvalidOperationException($"Invalid tile type: {tileType}")
    };

    private byte GetRockDurability() => (byte)rng.Next(1, 10);
}
