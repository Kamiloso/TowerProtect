using TowerProtect.Domain.Game.Tiles;

namespace TowerProtect.Domain.Game.Tiles.Specific;

public sealed class StoneTile() : Tile(Initial.Durability)
{
    public override int Toughness => int.MaxValue;

    public static class Initial
    {
        public const int Durability = 1;
    }
}
