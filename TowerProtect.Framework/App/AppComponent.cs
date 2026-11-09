using Microsoft.Xna.Framework;
using TowerProtect.App.Scopes;
using TowerProtect.App.Root.App;
using System;
using TowerProtect.Domain.Config;

namespace TowerProtect.Framework.App;

internal class AppComponent(
    Game game,
    Dynamic<IAppRoot> appRoot,
    LoopConfig cfg) : GameComponent(game)
{
    private IAppRoot? _ref = null;

    public override void Update(GameTime gameTime)
    {
        if (!ReferenceEquals(_ref, appRoot.Current))
        {
            _ref = appRoot.Current;
            _ref.Start();
        }

        double realDeltaTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
        double safeDeltaTime = Math.Min(realDeltaTime, cfg.MaxDeltaTime);

        appRoot.Current.Update(safeDeltaTime);

        base.Update(gameTime);
    }
}
