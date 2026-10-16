using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using TowerProtect.App.Backend;
using TowerProtect.App.Client;
using TowerProtect.App.Engine;
using TowerProtect.App.Root.App;
using TowerProtect.App.Root.Display;
using TowerProtect.Domain.Game.Maps;
using TowerProtect.Domain.Rendering;
using TowerProtect.Framework.App;
using TowerProtect.Framework.Input;
using TowerProtect.Framework.Rendering;
using TowerProtect.MonoGame;
using TowerProtect.Registry;
using TowerProtect.Scopes;

namespace TowerProtect;

public static class Program
{
    [STAThread]
    static void Main()
    {
        var srv = new ServiceCollection();

        srv.InitializeScopes();
        srv.InitializeMonoGame();

        srv.AddMonoGameServices();
        srv.AddSingletonServices();
        srv.AddScopedServices();

        using var provider = srv.BuildServiceProvider();
        using var game = provider.GetRequiredService<Game>();

        game.AttachComponents(provider);
        game.Run();
    }

    private static void AddMonoGameServices(this IServiceCollection srv)
    {
        // --- Adding components in execution order ---

        // 1. Early
        srv.AddMonoGameSingleton<ScopeComponent>();
        srv.AddMonoGameSingleton<InputComponent>();

        // 2. Logic
        srv.AddMonoGameSingleton<AppComponent>();

        // 3. Rendering
        srv.AddMonoGameSingleton<ScreenResizeComponent>();
        srv.AddMonoGameSingleton<RenderingComponent>();
        srv.AddMonoGameSingleton<LetterboxComponent>();
    }

    private static void AddSingletonServices(this IServiceCollection srv)
    {
        // Rendering
        srv.AddSingleton<ICameraView, CameraView>();

        // Input / Output
        srv.ReassignSingleton<IInputSystem, InputComponent>();
    }

    private static void AddScopedServices(this IServiceCollection srv)
    {
        // Roots
        srv.UseFullScoped<IAppRoot, LocalAppRoot, RemoteAppRoot, MenuAppRoot>();
        srv.UseFullScoped<IDisplayRoot, GameplayDisplayRoot, MenuDisplayRoot>();

        // Server-Side
        srv.UseLocalScoped<IBackendManager, BackendManager>();
        srv.UseLocalScoped<IMapProvider, MapProvider>();

        // Client-Side
        srv.UseGameplayScoped<IClientManager, ClientManager>();
        srv.UseGameplayScoped<IClientState, ClientState>();
    }
}
