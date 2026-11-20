using System;
using TowerProtect.Domain.Backend.Tiles.Abstract;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class MagicNestTile(Random rng) : BreakableTile(rng, (5, 8))
{
    public override TileType Type => TileType.MagicNest;
}
