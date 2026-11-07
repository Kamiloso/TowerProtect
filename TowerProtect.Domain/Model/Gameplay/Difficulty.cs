using System;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Exceptions;
using static TowerProtect.Domain.Config.DifficultyConfig;

namespace TowerProtect.Domain.Model.Gameplay;

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
    public static Difficulty GetByScore(int score)
    {
        foreach (var difficulty in Enum.GetValues<Difficulty>().Reverse())
        {
            if (score >= Difficulties[difficulty].MinScore)
                return difficulty;
        }

        throw new UnreachableException();
    }

    public static (int Min, int Max) GetScoreRange(this Difficulty self)
    {
        int min = Difficulties[self].MinScore;
        int max = Difficulties[self].IsLast
            ? int.MaxValue
            : Difficulties[self + 1].MinScore - 1;

        return (min, max);
    }

    public static IEnumerable<EntityType> GetEnemyBurst(this Difficulty self, Random rng)
    {
        var settings = Difficulties[self];

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

    public static double GetWaitingTime(this Difficulty self, Random rng)
    {
        var settings = Difficulties[self];
        var (min, max) = settings.WaitingTimeRange;

        return max > min
            ? min + rng.NextDouble() / (max - min)
            : min;
    }
}
