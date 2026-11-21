using System.Diagnostics;

namespace TowerProtect.Domain.Backend.Entities.Control;

public class WanderController : IEntityController
{
    public bool InputReceived => true;

    public double Rotation => _sw.ElapsedMilliseconds / 1000.0;
    public bool IsMoving => true;
    public bool IsShooting => true;

    // TODO: implement a better wandering algorithm, this is just a placeholder for now

    private readonly Stopwatch _sw = Stopwatch.StartNew();
}
