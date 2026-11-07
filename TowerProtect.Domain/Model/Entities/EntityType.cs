using System;

namespace TowerProtect.Domain.Model.Entities;

public enum EntityType : byte
{
    None = default,
    Player = 1,
    Animal = 2,
    Monster = 3,
    Insect = 4,
    Sniper = 5,
    Operator = 6,
    Drone = 7,
    Projectile = 8,
    Fruit = 9,
    Nest = 10,
}

public static class EntityTypeExtensions
{
    public static string GetTextureName(this EntityType self)
        => Enum.GetName(self) ?? Assets.Unknown;

    public static bool IsSimpleRender(this EntityType self)
        => self is EntityType.Player or EntityType.Animal or EntityType.Monster;

    public static int GetKillScore(this EntityType self)
        => 0; // TODO: configure
}
