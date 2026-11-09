namespace TowerProtect.Domain.Config;

public sealed record ScreenConfig
{
    public float MinAspectRatio { get; init; } = 0.5f;
    public float MaxAspectRatio { get; init; } = 2.0f;
}
