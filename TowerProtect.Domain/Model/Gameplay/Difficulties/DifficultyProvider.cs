using System.Collections.Generic;

namespace TowerProtect.Domain.Model.Gameplay.Difficulties;

public class DifficultyProvider(IReadOnlyDictionary<Difficulty, DifficultySettings> source)
{
    private readonly Dictionary<Difficulty, DifficultySettings> _dict = new(source);

    public Difficulty GetDifficulty(int score)
    {
        foreach (var (difficulty, settings) in _dict)
        {
            if (settings.ContainsScore(score))
                return difficulty;
        }

        throw new KeyNotFoundException($"Cannot map score: {score}");
    }

    public DifficultySettings GetSettings(int score)
        => GetSettings(GetDifficulty(score));

    public DifficultySettings GetSettings(Difficulty difficulty)
    {
        if (!_dict.TryGetValue(difficulty, out var settings))
            throw new KeyNotFoundException($"Cannot map difficulty: {difficulty}");

        return settings;
    }
}
