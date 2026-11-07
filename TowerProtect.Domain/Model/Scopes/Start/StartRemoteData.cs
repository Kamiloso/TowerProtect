namespace TowerProtect.Domain.Model.Scopes.Start;

public record StartRemoteData : StartData
{
    public override ScopeType Type => ScopeType.Remote;
}
