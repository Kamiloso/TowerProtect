using System;
using TowerProtect.Domain.Client.Rendering.Model;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Model.Gameplay.Tiles;

public enum TileType : byte
{
    Air = default,
    Wall = 1,
    Stone = 2,
    Cracked = 3,
    Water = 4,
    MagicStone = 5,

    SpawnAnimal = 201,
    SpawnMonster = 202,
    SpawnFruit = 203,
    SpawnMagicNest = 204,
}

public static class TileTypeExtensions
{
    public static string GetTextureName(this TileType self)
        => !IsSpawn(self) && Enum.GetName(self) is { } name ? name : Assets.Unknown;

    public static bool IsSolid(this TileType self)
        => self is TileType.Wall or TileType.Stone or TileType.Cracked or TileType.MagicStone;

    public static bool IsUnsolid(this TileType self)
        => false;

    public static bool IsFluid(this TileType self)
        => self is TileType.Air or TileType.Water;

    public static bool IsInvisible(this TileType self)
        => self is TileType.Air;

    public static bool IsContoured(this TileType self)
        => self.GetContourLevel() > 0;

    public static bool IsSpawn(this TileType self)
        => (int)self > 200;

    public static Layer GetDisplayLayer(this TileType self) => 0 switch
    {
        _ when self.IsSolid() => Layer.Solid,
        _ when self.IsUnsolid() => Layer.Unsolid,
        _ when self.IsFluid() => Layer.Fluid,
        _ => Layer.Default
    };

    public static int GetContourLevel(this TileType self) => 0 switch
    {
        _ when self.IsSolid() => 2,
        _ when self.IsUnsolid() || self.IsSpawn() || self is TileType.Air => 1,
        _ => 0
    };

    public static Vec2? GetCollider(this TileType self) => 0 switch
    {
        _ when self.IsSolid() => Vec2.One,
        _ => null
    };
}
