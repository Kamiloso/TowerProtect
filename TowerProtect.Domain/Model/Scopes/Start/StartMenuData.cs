namespace TowerProtect.Domain.Model.Scopes.Start;

public record StartMenuData : StartData
{
    public override ScopeType Type => ScopeType.Menu;
}
