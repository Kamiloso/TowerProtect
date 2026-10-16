using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerProtect.Framework.Rendering;

internal class LetterboxComponent(Game game) : DrawableGameComponent(game)
{
    private SpriteBatch _spriteBatch = null!;
    private Texture2D _pixel = null!;

    private const float MinRatio = 0.5f;
    private const float MaxRatio = 2.0f;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData([Color.Black]);

        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        int width = GraphicsDevice.Viewport.Width;
        int height = GraphicsDevice.Viewport.Height;
        float currentRatio = (float)width / height;

        if (currentRatio < MinRatio || currentRatio > MaxRatio)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);

            if (currentRatio > MaxRatio)
            {
                int targetWidth = (int)(height * MaxRatio);
                int boxWidth = (width - targetWidth) / 2;

                _spriteBatch.Draw(_pixel, new Rectangle(boxWidth - width, 0, width, height), Color.Black);
                _spriteBatch.Draw(_pixel, new Rectangle(width - boxWidth, 0, width, height), Color.Black);
            }
            else if (currentRatio < MinRatio)
            {
                int targetHeight = (int)(width / MinRatio);
                int boxHeight = (height - targetHeight) / 2;

                _spriteBatch.Draw(_pixel, new Rectangle(0, boxHeight - height, width, height), Color.Black);
                _spriteBatch.Draw(_pixel, new Rectangle(0, height - boxHeight, width, height), Color.Black);
            }

            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }
}
