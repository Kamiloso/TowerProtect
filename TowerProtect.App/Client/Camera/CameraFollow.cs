using System;
using AutoInterfaceAttributes;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Inventions.Rendering;
using TowerProtect.Domain.Model.Vectors;
using static TowerProtect.Domain.Config.CameraConfig;

namespace TowerProtect.Domain.Display.Camera;

partial interface ICameraFollow : IUpdateAware;

[AutoInterface]
internal class CameraFollow(ICameraView cameraView) : ICameraFollow
{
    public bool Enabled { get; set; }
    public Vec2 Target { get; set; }

    private Vec2 _velocity;

    public void Update(double deltaTime)
    {
        if (!Enabled) return;

        double smoothTime = Math.Max(0.0001, SmoothTime);
        double omega = 2.0 / smoothTime;
        double x = omega * deltaTime;

        double exp = Math.Exp(-x);

        Vec2 position = cameraView.Center;
        Vec2 change = position - Target;
        Vec2 temp = (_velocity + change * omega) * deltaTime;

        _velocity = (_velocity - temp * omega) * exp;
        cameraView.Center = Target + (change + temp) * exp;
    }
}
