namespace TowerProtect.Domain.Model.Scopes;

public record StartMenuData(
    GameplayResultData? OpenInfo = null) : StartData
{
    public override ScopeType Type => ScopeType.Menu;
}

public record GameplayResultData(int? FinalScore);
