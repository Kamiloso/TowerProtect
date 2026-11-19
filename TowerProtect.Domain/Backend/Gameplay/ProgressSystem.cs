using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Model.Gameplay.Difficulties;
using TowerProtect.Domain.Model.Gameplay.Entities;

namespace TowerProtect.Domain.Backend.Gameplay;

[AutoInterface]
internal class ProgressSystem(
    DifficultyConfig cfg,
    Random rng) : IProgressSystem
{
    public int Score { get; set; }
    public Difficulty Difficulty => DifficultyExtensions.GetByScore(cfg.Difficulties, Score);

    public IEnumerable<EntityType> NextEnemyBurst()
        => Difficulty.GetEnemyBurst(cfg.Difficulties, rng);

    public double NextWaitingTime()
        => Difficulty.GetWaitingTime(cfg.Difficulties, rng);
}
