using System;

namespace TowerProtect.Domain.Game.Tiles.Model;

public enum TileType
{
    Air = default,
    Stone = 1,
    Fruit = 2,
    Test = 3,
}

public static class TileTypeExtensions
{
    public static string GetName(this TileType self)
        => Enum.GetName(self) ?? Literals.Unknown;

    public static bool IsTransparent(this TileType self)
        => self == TileType.Air;
}
