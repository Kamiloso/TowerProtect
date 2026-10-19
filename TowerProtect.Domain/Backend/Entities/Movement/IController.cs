namespace TowerProtect.Domain.Backend.Entities.Movement;

public interface IController
{
    double Rotation { get; }
    bool IsMoving { get; }
    bool IsShooting { get; }
}
