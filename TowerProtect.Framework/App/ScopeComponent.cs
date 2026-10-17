using Microsoft.Xna.Framework;
using System;
using TowerProtect.App.Interfaces;
using TowerProtect.App.Scopes;
using TowerProtect.Domain.Model.Scopes;
using TowerProtect.Domain.Rendering;

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
        if (inputSystem.IsKeyDown("Space")) // TODO: temporary, remove
        {
            switch (Random.Shared.Next(3))
            {
                case 0: scopeSwitcher.SwitchScope(new StartLocalData("Username")); break;
                case 1: scopeSwitcher.SwitchScope(new StartRemoteData("Username", "Password", "Address")); break;
                case 2: scopeSwitcher.SwitchScope(new StartMenuData()); break;
            }
        } // end temporary

        if (scopeExchanger.ExchangeScopeIfDelayed())
        {
            cameraView.ResetTransform();
        }

        base.Update(gameTime);
    }
}
