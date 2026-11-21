using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Model.Gameplay.Difficulties;

public record DifficultySettings
{
    public required (int Min, int Max) ScoreRange { get; init; }
    public required (double Min, double Max) WaitingTimeRange { get; init; }
    public required (int Min, int Max) BurstSizeRange { get; init; }
    public required ImmutableArray<(double Chance, EntityType Entity)> Odds { get; init; }

    public bool ContainsScore(int score)
        => score >= ScoreRange.Min && score < ScoreRange.Max;

    public double GetWaitingTime(Random rng)
        => rng.NextDouble() * (WaitingTimeRange.Max - WaitingTimeRange.Min) + WaitingTimeRange.Min;

    public IEnumerable<EntityType> GetEnemyBurst(Random rng)
    {
        int burstSize = rng.Next(BurstSizeRange.Min, BurstSizeRange.Max);

        for (int i = 0; i < burstSize; i++)
        {
            var probe = new ChanceProbe(rng);
            var nextEntity = EntityType.None;

            foreach (var (chance, entity) in Odds)
            {
                probe.If(chance, () => nextEntity = entity);
            }

            yield return nextEntity;
        }
    }
}
