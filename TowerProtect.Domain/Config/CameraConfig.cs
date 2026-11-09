namespace TowerProtect.Domain.Config;

public sealed record CameraConfig
{
    public double DefaultSide { get; init; } = 20.0;
    public double CullingRangeMultiplier { get; init; } = 1.5;
    public double SmoothTime { get; init; } = 0.2;
    public double ZoomStep { get; init; } = 0.2;
    public int MinZoomPoint { get; init; } = -4;
    public int MaxZoomPoint { get; init; } = 4;
}
