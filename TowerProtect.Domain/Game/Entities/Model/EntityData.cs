using System;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Game.Entities.Model;

public record EntityData(
    Guid Guid,
    EntityType Type,
    Vec2 Position,
    double Rotation);
