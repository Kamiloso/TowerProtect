using System.Diagnostics;

namespace TowerProtect.Domain.Backend.Entities.Movement;

internal class EnemyController : IController
{
    private readonly Stopwatch _sw = Stopwatch.StartNew();

    public double Rotation => _sw.ElapsedMilliseconds / 1000.0;
    public bool IsMoving => true;
    public bool IsShooting => true;

    // TODO: Implement enemy AI logic
}
