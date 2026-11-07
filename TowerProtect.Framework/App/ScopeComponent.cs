using Microsoft.Xna.Framework;
using TowerProtect.App.Scopes;
using TowerProtect.Domain.Rendering.Camera;
using TowerProtect.App.Client.Interfaces;
using TowerProtect.Domain.Model.Scopes.Start;

namespace TowerProtect.Framework.App;

internal class ScopeComponent(
    Game game,
    IInputSystem inputSystem, // temorary
    IScopeSwitcher scopeSwitcher, // temporary
    IScopeExchanger scopeExchanger,
    ICameraView cameraView) : GameComponent(game)
{
    public override void Update(GameTime gameTime)
    {
        // TODO: Remove
        if (inputSystem.IsKeyDown("Space"))
            scopeSwitcher.SwitchScope(new StartLocalData());
        // END TEMPORARY

        if (scopeExchanger.ExchangeScopeIfDelayed())
            cameraView.ResetTransform();

        base.Update(gameTime);
    }
}
