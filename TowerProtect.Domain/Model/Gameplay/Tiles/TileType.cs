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
        => Enum.GetName(self) ?? Assets.Unknown;

    public static bool IsSolid(this TileType self)
        => self is TileType.Wall or TileType.Stone or TileType.Cracked or TileType.MagicStone;

    public static bool IsUnsolid(this TileType self)
        => IsInvisible(self);

    public static bool IsBelowAir(this TileType self)
        => self is TileType.Water;

    public static bool IsSpawn(this TileType self)
        => self is TileType.SpawnAnimal or TileType.SpawnMonster or TileType.SpawnFruit or TileType.SpawnMagicNest;

    public static bool IsInvisible(this TileType self)
        => self is TileType.Air || IsSpawn(self);

    public static Vec2? GetCollider(this TileType self)
        => self.IsSolid() ? Vec2.One : null;

    public static Layer GetDisplayLayer(this TileType self) => 0 switch
    {
        _ when self.IsSolid() => Layer.Solid,
        _ when self.IsUnsolid() => Layer.Unsolid,
        _ when self.IsBelowAir() => Layer.BelowAir,
        _ => default
    };

    public static int GetContourLevel(this TileType self) => 0 switch
    {
        _ when self.IsSolid() => 1,
        _ when self.IsBelowAir() => -1,
        _ => 0
    };
}
