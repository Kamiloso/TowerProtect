using TowerProtect.Domain.Model.Unions;

namespace TowerProtect.Domain.Backend.Entities.Control;

public class ManualController : IEntityController
{
    public bool InputReceived { get; private set; }

    public double Rotation { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsShooting { get; private set; }

    public void AcceptInput(InputUnion inputUnion)
    {
        InputReceived = true;

        Rotation = inputUnion.Rotation;
        IsMoving = inputUnion.IsMoving;
        IsShooting = inputUnion.IsShooting;
    }
}
