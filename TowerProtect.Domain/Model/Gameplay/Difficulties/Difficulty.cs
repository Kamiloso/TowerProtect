using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Exceptions;
using TowerProtect.Domain.Model.Gameplay.Entities;

using DifficultyProvider = System.Collections.Generic.IReadOnlyDictionary<
    TowerProtect.Domain.Model.Gameplay.Difficulties.Difficulty,
    TowerProtect.Domain.Model.Gameplay.Difficulties.DifficultySettings>;

namespace TowerProtect.Domain.Model.Gameplay.Difficulties;

public enum Difficulty
{
    None = 0,
    AnimalParadise = 1,
    MonsterInc = 2,
    InsectFactory = 3,
    // ...
}

public static class DifficultyExtensions
{
    public static Difficulty GetByScore(DifficultyProvider diffs, int score)
    {
        foreach (var difficulty in Enum.GetValues<Difficulty>().Reverse())
        {
            if (score >= diffs[difficulty].MinScore)
                return difficulty;
        }

        throw new UnreachableException();
    }

    public static (int Min, int Max) GetScoreRange(this Difficulty self, DifficultyProvider diffs)
    {
        int min = diffs[self].MinScore;
        int max = diffs[self].IsLast
            ? int.MaxValue
            : diffs[self + 1].MinScore - 1;

        return (min, max);
    }

    public static IEnumerable<EntityType> GetEnemyBurst(this Difficulty self, DifficultyProvider diffs, Random rng)
    {
        var settings = diffs[self];

        foreach (var _ in Enumerable.Range(0, settings.BurstSize))
        {
            var probe = new ChanceProbe(rng);
            var nextEntity = EntityType.None;

            foreach (var (chance, entity) in settings.Odds)
            {
                probe.If(chance, () => nextEntity = entity);
            }

            yield return nextEntity;
        }
    }

    public static double GetWaitingTime(this Difficulty self, DifficultyProvider diffs, Random rng)
    {
        var settings = diffs[self];
        var (min, max) = settings.WaitingTimeRange;

        return max > min
            ? min + rng.NextDouble() / (max - min)
            : min;
    }
}
