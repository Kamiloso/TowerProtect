namespace TowerProtect.Domain.Model.Scopes.Start;

public record StartLocalData : StartData
{
    public override ScopeType Type => ScopeType.Local;
}
