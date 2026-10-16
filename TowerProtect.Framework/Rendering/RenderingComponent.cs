using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TowerProtect.App.Scopes;
using TowerProtect.Domain.Tools;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Rendering;
using TowerProtect.Framework.Extensions;
using TowerProtect.Framework.Rendering.Model;
using TowerProtect.App.Root.Display;

namespace TowerProtect.Framework.Rendering;

internal class RenderingComponent(
    Game game,
    ICameraView cameraView,
    Dynamic<IDisplayRoot> displayRoot) : DrawableGameComponent(game)
{
    private RectangleF ScreenBounds => (RectangleF)GraphicsDevice.Viewport.Bounds;

    private SpriteBatch _spriteBatch = null!;
    private LazyProvider<string, Texture2D> _textures = null!;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _textures = new(key => Game.Content.Load<Texture2D>(key));

        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
		cameraView.AspectRatio = ScreenBounds.Width / ScreenBounds.Height;

        List<MonoSprite> sprites = [.. displayRoot.Current.DrawNextFrame()
            .Select(s => ToMonoSprite(s, cameraView))];

        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

		foreach (var sprite in sprites)
        {
            _spriteBatch.Draw(
                texture: sprite.Texture,
                position: sprite.CenterPosition,
                sourceRectangle: null,
                color: Color.White,
                rotation: (float)sprite.Rotation,
                origin: sprite.Origin,
                scale: sprite.Scale,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private MonoSprite ToMonoSprite(Sprite sprite, ICameraView cameraView)
    {
        Rect cameraBounds = cameraView.VisionArea;
        Rect screenBounds = ScreenBounds.ToRect();

        Rect cameraTextureArea = sprite.TextureArea;
        Rect screenTextureArea = Rect.MapRect(cameraTextureArea, cameraBounds, screenBounds);

        return new MonoSprite(
            _textures[sprite.TextureName],
            screenTextureArea.ToRectangleF(),
            sprite.Rotation
        );
    }
}
