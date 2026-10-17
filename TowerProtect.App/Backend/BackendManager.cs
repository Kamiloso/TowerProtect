using AutoInterfaceAttributes;
using TowerProtect.Domain.Game.Entities;
using TowerProtect.Domain.Backend.Tiles;
using TowerProtect.Domain.Interfaces;
using TowerProtect.Domain.Backend.Maps;

namespace TowerProtect.App.Backend;

partial interface IBackendManager : IStartAware, IUpdateAware;

[AutoInterface]
internal class BackendManager : IBackendManager
{
    private readonly Tilemap _tilemap;
    private readonly EntityBag _entityBag;

    public BackendManager(IMapProvider mapProvider)
    {
        Map map = mapProvider.GetMap(null);

        _tilemap = new Tilemap(map.Range, map.CreateTileAt);
        _entityBag = new();
    }

    public void Start() { }

    public void Update(double deltaTime)
    {
        _tilemap.Update(deltaTime);
        _entityBag.Update(deltaTime);
    }
}
