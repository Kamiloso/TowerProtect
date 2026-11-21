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
    private readonly DifficultyProvider _difficulties = cfg.Difficulties;

    public int Score { get; set; }
    public Difficulty Difficulty => _difficulties.GetDifficulty(Score);
    public DifficultySettings DifficultySettings => _difficulties.GetSettings(Score);

    public IEnumerable<EntityType> NextEnemyBurst()
        => DifficultySettings.GetEnemyBurst(rng);

    public double NextWaitingTime()
        => DifficultySettings.GetWaitingTime(rng);
}
