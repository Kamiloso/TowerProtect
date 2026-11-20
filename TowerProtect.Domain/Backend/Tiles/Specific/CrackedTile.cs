using System;
using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class CrackedTile(Random rng) : BreakableTile(rng, (3, 9))
{
    public override TileType Type => TileType.Cracked;
}
