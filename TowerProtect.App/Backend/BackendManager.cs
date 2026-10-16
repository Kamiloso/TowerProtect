using AutoInterfaceAttributes;
using TowerProtect.Domain.Game.Entities;
using TowerProtect.Domain.Game.Maps;
using TowerProtect.Domain.Game.Tiles;
using TowerProtect.Domain.Interfaces;

namespace TowerProtect.App.Backend;

[AutoInterface]
internal class BackendManager : IBackendManager, IUpdateAware
{
    private readonly EntityBag _entityBag = new();
    private readonly Tilemap _tilemap;

    public BackendManager(IMapProvider mapProvider)
    {
        var map = mapProvider.GetMap(null);
        _tilemap = new Tilemap(map.Range, map.CreateTileAt);
    }

    public void Update(double deltaTime)
    {
        _entityBag.Update(deltaTime);
        _tilemap.Update(deltaTime);
    }
}
