using Microsoft.Xna.Framework;
using System;
using TowerProtect.App.Root.App;
using TowerProtect.App.Scopes;
using TowerProtect.Domain.Config;

namespace TowerProtect.Framework.App;

internal class AppComponent(
    Game game,
    Dynamic<IAppRoot> appRoot,
    LoopConfig cfg) : GameComponent(game)
{
    private IAppRoot? _prevAppRoot;

    public override void Update(GameTime gameTime)
    {
        if (!ReferenceEquals(_prevAppRoot, appRoot.Current))
        {
            _prevAppRoot = appRoot.Current;
            _prevAppRoot.Start();
        }

        double realDeltaTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
        double safeDeltaTime = Math.Clamp(realDeltaTime, cfg.MinDeltaTime, cfg.MaxDeltaTime);

        appRoot.Current.Update(safeDeltaTime);

        base.Update(gameTime);
    }
}
