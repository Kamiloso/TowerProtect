using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerProtect.Framework;

internal class MainGame : Game
{
    public GraphicsDeviceManager Graphics { get; }
    public Viewport Viewport => GraphicsDevice.Viewport;

    public MainGame()
    {
        Graphics = new GraphicsDeviceManager(this)
        {
            IsFullScreen = false,
            HardwareModeSwitch = false,
            SynchronizeWithVerticalRetrace = true,
        };

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
    }
}
