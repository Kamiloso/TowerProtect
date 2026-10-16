using TowerProtect.Domain.Model.Scopes;

namespace TowerProtect.App.Scopes;

public interface IScopeSwitcher
{
    void SwitchScope<T>(T startData) where T : StartData;
}
