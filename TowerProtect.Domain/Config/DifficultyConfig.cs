using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;
using System.Collections.Generic;

namespace TowerProtect.Domain.Config;

public sealed record DifficultyConfig
{
    public DifficultyProvider Difficulties { get; init; } =
        new(new Dictionary<Difficulty, DifficultySettings>()
    {
        [Difficulty.None] = new()
        {
            ScoreRange = (int.MinValue, 0),
            WaitingTimeRange = (1.0, 2.0),
            BurstSizeRange = (0, 1),
            Odds = [],
        },

        [Difficulty.AnimalParadise] = new()
        {
            ScoreRange = (0, 10),
            WaitingTimeRange = (1.0, 2.0),
            BurstSizeRange = (4, 5),
            Odds = [
                (1.0, EntityType.Animal),
            ],
        },

        [Difficulty.MonsterInc] = new()
        {
            ScoreRange = (10, 50),
            WaitingTimeRange = (1.0, 2.0),
            BurstSizeRange = (4, 5),
            Odds = [
                (0.2, EntityType.Animal),
                (0.8, EntityType.Monster),
            ],
        },

        [Difficulty.InsectFactory] = new()
        {
            ScoreRange = (50, int.MaxValue),
            WaitingTimeRange = (1.0, 2.0),
            BurstSizeRange = (6, 7),
            Odds = [
                (0.2, EntityType.Monster),
                (0.8, EntityType.Insect),
            ],
        },
    });
}
