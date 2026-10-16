using TowerProtect.Domain.Game.Tiles;

namespace TowerProtect.Domain.Game.Tiles.Specific;

public sealed class AirTile() : Tile(Initial.Durability)
{
    public override int Toughness => 0;

    public static class Initial
    {
        public const int Durability = 1;
    }
}
