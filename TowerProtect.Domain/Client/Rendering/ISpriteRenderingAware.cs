using System.Collections.Generic;
using TowerProtect.Domain.Client.Rendering.Sprites;
using TowerProtect.Domain.Client.Rendering.Camera;

namespace TowerProtect.Domain.Client.Rendering;

public interface ISpriteRenderingAware
{
    void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView);
}
