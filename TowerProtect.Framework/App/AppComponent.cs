using Microsoft.Xna.Framework;
using TowerProtect.App.Scopes;
using TowerProtect.App.Root.App;

namespace TowerProtect.Framework.App;

internal class AppComponent(Game game, Dynamic<IAppRoot> appRoot) : GameComponent(game)
{
    public override void Update(GameTime gameTime)
    {
        double deltaTime = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
        appRoot.Current.Update(deltaTime);

        base.Update(gameTime);
    }
}
