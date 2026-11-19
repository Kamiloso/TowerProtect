using System.Collections.Generic;
using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Config;

public sealed record DifficultyConfig
{
    public IReadOnlyDictionary<Difficulty, DifficultySettings> Difficulties { get; init; } =
        new Dictionary<Difficulty, DifficultySettings>()
    {
        [Difficulty.None] = new()
        {
            MinScore = int.MinValue,
            WaitingTimeRange = (1.0, 2.0),
            BurstSize = 0,
            Odds = [],
        },

        [Difficulty.AnimalParadise] = new()
        {
            MinScore = 0,
            WaitingTimeRange = (1.0, 2.0),
            BurstSize = 4,
            Odds = [
                    (1.0, EntityType.Animal),
                ],
        },

        [Difficulty.MonsterInc] = new()
        {
            MinScore = 10,
            WaitingTimeRange = (1.0, 2.0),
            BurstSize = 4,
            Odds = [
                    (0.2, EntityType.Animal),
                    (0.8, EntityType.Monster),
                ],
        },

        [Difficulty.InsectFactory] = new()
        {
            MinScore = 50,
            IsLast = true,
            WaitingTimeRange = (1.0, 2.0),
            BurstSize = 6,
            Odds = [
                    (0.2, EntityType.Monster),
                    (0.8, EntityType.Insect),
                ],
        },
    };
}
