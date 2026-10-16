using TowerProtect.Domain.Game.Maps;

namespace TowerProtect.Domain.Game.Maps.Specific;

public sealed class PlaceholderMap : Map
{
    public override int Range => 10;

    protected override string Template => """
        X X X X X X X X X X X X X X X X X X X X X
        X . . . . . X . . . . . . . X . . . . . X
        X . X X X . X . X X X X X . X . X X X . X
        X . X . . . . . . . X . . . . . . . X . X
        X . X . X X X X X . X . X X X X X . X . X
        X . X . X . . . . . X . . . . . X . X . X
        X . X . X . X X X X X X X X X . X . X . X
        X . X . X . X . . . . . . . X . X . X . X
        X . X . X . X . . . . . . . X . X . X . X
        X . . . . . X . . . . . . . X . . . . . X
        X X X X X . . . . . . . . . . . X X X X X
        X . . . . . X . . . . . . . X . . . . . X
        X . X . X . X . . . . . . . X . X . X . X
        X . X . X . X . . . . . . . X . X . X . X
        X . X . X . X X X X X X X X X . X . X . X
        X . X . X . . . . . X . . . . . X . X . X
        X . X . X X X X X . X . X X X X X . X . X
        X . X . . . . . . . X . . . . . . . X . X
        X . X X X . X . X X X X X . X . X X X . X
        X . . . . . X . . . . . . . X . . . . . X
        X X X X X X X X X X X X X X X X X X X X X
        """;
}