using AutoInterfaceAttributes;
using TowerProtect.App.Interfaces;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Inventions.Rendering;
using static TowerProtect.Domain.Config.CameraConfig;

namespace TowerProtect.Domain.Display.Camera;

partial interface ICameraZoomControl : IUpdateAware;

[AutoInterface]
internal class CameraZoomControl(
    IInputSystem inputSystem,
    ICameraView cameraView) : ICameraZoomControl
{
    private int _cameraZoomLevel = 0;

    public void Update(double deltaTime)
    {
        if (inputSystem.IsKeyPressed("LeftControl"))
        {
            if (inputSystem.ScrollSign > 0 && _cameraZoomLevel < MaxZoomPoint)
            {
                _cameraZoomLevel++;
                cameraView.Zoom += ZoomStep;
            }

            if (inputSystem.ScrollSign < 0 && _cameraZoomLevel > MinZoomPoint)
            {
                _cameraZoomLevel--;
                cameraView.Zoom -= ZoomStep;
            }
        }
    }
}
