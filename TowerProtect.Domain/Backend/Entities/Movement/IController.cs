namespace TowerProtect.Domain.Backend.Entities.Movement;

public interface IController
{
    bool InputReceived { get; }

    // Input properties
    double Rotation { get; }
    bool IsMoving { get; }
    bool IsShooting { get; }
}
