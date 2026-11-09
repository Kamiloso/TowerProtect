namespace TowerProtect.Domain.Config;

public sealed record LoopConfig
{
    public double MaxDeltaTime { get; init; } = 0.1;
    public double InputSendPeriod { get; init; } = 0.05;
    public double BroadcastSendPeriod { get; init; } = 0.05;
}
