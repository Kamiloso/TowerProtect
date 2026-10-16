using System.Collections.Generic;
using TowerProtect.Domain.Rendering;

namespace TowerProtect.Domain.Interfaces;

public interface ISpriteRenderingAware
{
    void RenderSprites(List<Sprite> targetSprites, ICameraView cameraView);
}
