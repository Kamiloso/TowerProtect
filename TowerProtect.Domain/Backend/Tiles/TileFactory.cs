using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Backend.Tiles.Specific;
using TowerProtect.Domain.Backend.Tiles.Specific.Spawn;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles;

[AutoInterface]
internal class TileFactory(Random rng) : ITileFactory
{
    public Tile CreateTile(TileType tileType) => tileType switch
    {
        TileType.Air => AirTile.Instance,
        TileType.Wall => WallTile.Instance,
        TileType.Stone => StoneTile.Instance,
        TileType.Cracked => new CrackedTile(rng),
        TileType.Water => WaterTile.Instance,
        TileType.MagicStone => MagicStoneTile.Instance,
        TileType.SpawnAnimal => SpawnAnimalTile.Instance,
        TileType.SpawnMonster => SpawnMonsterTile.Instance,
        TileType.SpawnFruit => SpawnFruitTile.Instance,
        TileType.SpawnMagicNest => SpawnMagicNestTile.Instance,
        _ => throw new InvalidOperationException($"Invalid tile type: {tileType}")
    };
}
