using System;
using static TowerProtect.Domain.Backend.Worldgen.WorldgenConst;

namespace TowerProtect.Domain.Backend.Worldgen.Rooms.Abstract.Cell;

internal abstract class CellRoom(Random rng, string pattern)
    : Room(rng, range: CellRange, pattern);
