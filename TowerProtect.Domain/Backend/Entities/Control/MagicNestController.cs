namespace TowerProtect.Domain.Backend.Entities.Control;

public class MagicNestController : IEntityController
{
    public bool InputReceived => true;

    public double Rotation => 0.0;
    public bool IsMoving => false;
    public bool IsShooting => false;
}
