using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Gameplay;

namespace TowerProtect.Domain.Backend.Gameplay;

[AutoInterface]
internal class ProgressSystem(Random rng) : IProgressSystem
{
    public int Score { get; set; }
    public Difficulty Difficulty => DifficultyExtensions.GetByScore(Score);

    public IEnumerable<EntityType> NextEnemyBurst()
        => Difficulty.GetEnemyBurst(rng);

    public double NextWaitingTime()
        => Difficulty.GetWaitingTime(rng);
}
