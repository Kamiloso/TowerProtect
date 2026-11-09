using AutoInterfaceAttributes;
using TowerProtect.Domain.Config;

namespace TowerProtect.Domain.Rendering.Camera;

partial interface ICameraZoom;

[AutoInterface]
public class CameraZoom(
    ICameraView cameraView,
    CameraConfig cfg) : ICameraZoom
{
    private int _zoomLevel = 0;

    public void ChangeZoom(int zoomDelta)
    {
        if (zoomDelta > 0 && _zoomLevel < cfg.MaxZoomPoint)
        {
            _zoomLevel++;
            cameraView.Zoom += cfg.ZoomStep;
        }

        if (zoomDelta < 0 && _zoomLevel > cfg.MinZoomPoint)
        {
            _zoomLevel--;
            cameraView.Zoom -= cfg.ZoomStep;
        }
    }
}
