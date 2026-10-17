using Microsoft.Xna.Framework;
using TowerProtect.App.Scopes;
using TowerProtect.App.Root.App;

namespace TowerProtect.Framework.App;

internal class AppComponent(Game game, Dynamic<IAppRoot> appRoot) : GameComponent(game)
{
    private IAppRoot? _ref = null;

    public override void Update(GameTime gameTime)
    {
        if (!ReferenceEquals(_ref, appRoot.Current))
        {
            _ref = appRoot.Current;
            _ref.Start();
        }

        double deltaTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
        appRoot.Current.Update(deltaTime);

        base.Update(gameTime);
    }
}
