namespace TowerProtect.Domain.Backend.Entities.Movement;

public class ManualController : IController
{
    public double Rotation { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsShooting { get; private set; }

    public void Update(double rotation, bool isMoving, bool isShooting)
    {
        Rotation = rotation;
        IsMoving = isMoving;
        IsShooting = isShooting;
    }
}
