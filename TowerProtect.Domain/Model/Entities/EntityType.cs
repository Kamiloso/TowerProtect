using System;

namespace TowerProtect.Domain.Model.Entities;

public enum EntityType
{
    Player = default,
    Monster = 1,
}

public static class EntityTypeExtensions
{
    public static string GetTextureName(this EntityType self)
        => Enum.GetName(self) ?? Assets.Unknown;
}
