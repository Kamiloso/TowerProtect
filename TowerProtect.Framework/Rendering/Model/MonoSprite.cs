using Microsoft.Xna.Framework.Graphics;

namespace TowerProtect.Framework.Rendering.Model;

internal record MonoSprite(
    Texture2D Texture,
    RectangleF TextureArea,
    double Rotation)
{
    public Vector2 Origin => new(Texture.Width / 2f, Texture.Height / 2f);

    public Vector2 Scale => new(
        TextureArea.Width / Texture.Width,
        TextureArea.Height / Texture.Height
    );

    public Vector2 CenterPosition => new(
        TextureArea.X + TextureArea.Width / 2f,
        TextureArea.Y + TextureArea.Height / 2f
    );
}
