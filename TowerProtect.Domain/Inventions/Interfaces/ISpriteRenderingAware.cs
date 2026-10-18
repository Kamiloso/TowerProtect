using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Inventions.Rendering.Sprites;

namespace TowerProtect.Domain.Inventions.Interfaces;

public interface ISpriteRenderingAware
{
    void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView);
}
