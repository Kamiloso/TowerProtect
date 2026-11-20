using System;
using TowerProtect.Domain.Model.Gameplay.Tiles;

namespace TowerProtect.Domain.Backend.Worldgen.Rooms.Abstract.Cell.Gate;

internal abstract class FarmRoom(Random rng, char enemy) : GateRoom(rng,
    entrance: '+',
    pattern: """
        E . . . E
        . . . . .
        . . E . .
        . . . . .
        E . . . E
        """)
{
    protected override TileType ParseTile(char c)
        => base.ParseTile(c == 'E' ? enemy : c);
}
