using System;
using TowerProtect.Domain.Client.Rendering.Model;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Model.Gameplay.Entities;

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
    EnemyProjectile = 9,
    Fruit = 10,
    MagicNest = 11,
}

public static class EntityTypeExtensions
{
    public static string GetTextureName(this EntityType self)
        => Enum.GetName(self) ?? Assets.Unknown;

    public static bool IsTiny(this EntityType self)
        => self is EntityType.Projectile;

    public static bool IsSmall(this EntityType self)
        => self is EntityType.Insect or EntityType.Drone or EntityType.Fruit;

    public static bool IsMedium(this EntityType self)
        => self is EntityType.Player or EntityType.Animal or EntityType.Monster
        or EntityType.Sniper or EntityType.MagicNest;

    public static bool IsLarge(this EntityType self)
        => self is EntityType.Operator;

    public static bool IsControllable(this EntityType self)
        => self is EntityType.Player or EntityType.Animal or EntityType.Monster
        or EntityType.Insect or EntityType.Sniper or EntityType.Operator
        or EntityType.Drone or EntityType.MagicNest;

    public static bool IsShootable(this EntityType self)
        => self is EntityType.Projectile or EntityType.EnemyProjectile;

    public static bool HasSimpleRenderer(this EntityType self)
        => (self.IsControllable() && self is not EntityType.MagicNest) || self.IsShootable();

    public static bool HasCrystalRenderer(this EntityType self)
        => self is EntityType.MagicNest;

    public static double GetCrystalRotationSpeed(this EntityType self)
        => self is EntityType.MagicNest ? 2.0 : 0.0;

    public static Vec2 GetSize(this EntityType self)
        => new(self.GetSide());

    public static double GetSide(this EntityType self) => 0 switch
    {
        _ when self.IsTiny() => 0.4,
        _ when self.IsSmall() => 0.6,
        _ when self.IsMedium() => 0.8,
        _ when self.IsLarge() => 1.1,
        _ => 0.8
    };

    public static Layer GetDisplayLayer(this EntityType self, bool isMain) => 0 switch
    {
        _ when self is EntityType.Player => Layer.Player.SubLayer(isMain ? 10 : 0),
        _ when self.IsControllable() => Layer.Entity,
        _ when self is EntityType.Projectile => Layer.Projectile,
        _ when self is EntityType.EnemyProjectile => Layer.EnemyProjectile,
        _ => default
    };
}
