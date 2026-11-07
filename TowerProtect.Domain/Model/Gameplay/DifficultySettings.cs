using System.Collections.Immutable;
using TowerProtect.Domain.Model.Entities;

namespace TowerProtect.Domain.Model.Gameplay;

public record DifficultySettings
{
    // Score settings
    public required int MinScore { get; init; }
    public bool IsLast { get; init; } = false;

    // Spawning settings
    public required (double Min, double Max) WaitingTimeRange { get; init; }
    public required int BurstSize { get; init; }
    public required ImmutableArray<(double Chance, EntityType Entity)> Odds { get; init; }
}
