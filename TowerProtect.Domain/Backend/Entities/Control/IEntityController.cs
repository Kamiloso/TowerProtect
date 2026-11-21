namespace TowerProtect.Domain.Backend.Entities.Control;

public interface IEntityController
{
    bool InputReceived => true;

    double Rotation { get; }
    bool IsMoving { get; }
    bool IsShooting { get; }
}
