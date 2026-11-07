namespace TowerProtect.Domain.Backend.Entities.Input;

public interface IEntityController
{
    bool InputReceived { get; }

    // Input properties
    double Rotation { get; }
    bool IsMoving { get; }
    bool IsShooting { get; }
}
