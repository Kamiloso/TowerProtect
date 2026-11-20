#pragma warning disable CS9107 // Argument capturing

using System;
using TowerProtect.Domain.Backend.Worldgen.Rooms.Abstract.Cell.Gate;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Worldgen.Rooms.Specific;

internal class FruitRoom(Random rng) : GateRoom(rng,
    pattern: """
        ? . . . ?
        # . . . #
        ? . . . ?
        # . . . #
        ? . . . ?
        """)
{
    protected override TileType ParseTile(char c) => base.ParseTile(c switch
    {
        '?' => rng.Next(3) < 2 ? 'o' : '.',
        _ => c,
    });
}
