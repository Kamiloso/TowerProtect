namespace TowerProtect.Domain.Config;

public sealed record LoopConfig
{
    public double MinDeltaTime { get; init; } = 1e-5; // no way someone will have 100 000 FPS
    public double MaxDeltaTime { get; init; } = 0.1;
    public double InputSendPeriod { get; init; } = 0.05;
    public double BroadcastSendPeriod { get; init; } = 0.05;
}
