using System.Collections.Immutable;
using TowerProtect.Domain.Game.Entities.Model;

namespace TowerProtect.Domain.Model.Dto;

public record FrameData(
    long Tick,
    ImmutableArray<EntityData> Entities);
