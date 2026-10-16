using System;

namespace TowerProtect.Domain.Game.Entities.Model;

public enum EntityType
{
    Player = default,
    Monster = 1,
}

public static class EntityTypeExtensions
{
    public static string GetName(this EntityType self)
        => Enum.GetName(self) ?? Literals.Unknown;
}
