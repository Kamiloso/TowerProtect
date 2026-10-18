using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using TowerProtect.App.Interfaces;
using TowerProtect.Domain.Model.Vectors;
using TowerProtect.Domain.Inventions.Rendering;

namespace TowerProtect.Framework.Diagnostics;

internal class DiagnosticsComponent(
    Game game,
    IInputSystem inputSystem,
    ICameraView cameraView) : DrawableGameComponent(game)
{
    private SpriteBatch _spriteBatch = null!;
    private readonly Process _process = Process.GetCurrentProcess();

    private TimeSpan _lastTime;
    private TimeSpan _lastTotalProcessorTime;
    private double _cpuUsage;

    private int _frameCount;
    private double _fps;
    private double _frameTime;

    private bool _enabled;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public override void Update(GameTime gameTime)
    {
        if (inputSystem.IsKeyDown("F3"))
            _enabled = !_enabled;

        if (_enabled)
            RefreshUpdateInfo(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        if (_enabled)
        {
            RefreshDrawInfo(gameTime);
            DrawInfo(gameTime);
        }

        base.Draw(gameTime);
    }

    private void RefreshUpdateInfo(GameTime gameTime)
    {
        if (_lastTime == TimeSpan.Zero)
        {
            _lastTime = gameTime.TotalGameTime;
            _lastTotalProcessorTime = _process.TotalProcessorTime;
            return;
        }

        TimeSpan currentTime = gameTime.TotalGameTime;
        TimeSpan currentTotalProcessorTime = _process.TotalProcessorTime;

        double totalMsPassed = (currentTime - _lastTime).TotalMilliseconds;

        if (totalMsPassed >= 1000.0)
        {
            double cpuUsedMs = (currentTotalProcessorTime - _lastTotalProcessorTime).TotalMilliseconds;

            _cpuUsage = (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)) * 100;
            _fps = _frameCount / (totalMsPassed / 1000.0);

            _frameCount = 0;
            _lastTime = currentTime;
            _lastTotalProcessorTime = currentTotalProcessorTime;
        }
    }

    private void RefreshDrawInfo(GameTime gameTime)
    {
        _frameCount++;
        _frameTime = gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    private void DrawInfo(GameTime gameTime)
    {
        _process.Refresh();

        Vec2 cameraPosition = cameraView.Center;
        long processMemoryMB = _process.WorkingSet64 / 1048576;
        long managedMemoryMB = GC.GetTotalMemory(false) / 1048576;
        int gc0 = GC.CollectionCount(0);
        int gc1 = GC.CollectionCount(1);
        int gc2 = GC.CollectionCount(2);

        string text = $"""
            FPS: {_fps:0}
            Frame Time: {_frameTime:0.00} ms
            Position: {cameraPosition}
            CPU: {_cpuUsage:0.0}%
            RAM (Process): {processMemoryMB} MB
            RAM (Managed): {managedMemoryMB} MB
            GC (0/1/2): {gc0} / {gc1} / {gc2}
            """;

        _spriteBatch.Begin();
        _spriteBatch.DrawString(MainGame.Font, text, new Vector2(10, 10), Color.Yellow);
        _spriteBatch.End();
    }
}
