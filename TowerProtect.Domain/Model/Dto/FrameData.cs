using System.Collections.Immutable;
using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.Domain.Model.Dto;

public record FrameData(
    long Tick,
    ImmutableArray<EntityData> Entities);
