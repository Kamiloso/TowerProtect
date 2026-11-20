using AutoInterfaceAttributes;
using TowerProtect.Domain.Backend.Physics;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Tiles;

partial interface ICollisionGrid : IStartAware;

[AutoInterface]
internal class CollisionGrid(
    ITilemap tilemap,
    IPhysicsSystem physicsSystem) : ICollisionGrid
{
    public void Start()
    {
        var tilemapData = tilemap.CollectTilemapData();
        var populator = tilemapData.GetPopulator();

        Area.Iterate(tilemapData.Range, pos =>
        {
            var collider = populator(pos).GetCollider();
            physicsSystem.PlaceStaticCollider(pos, collider);
        });

        tilemap.OnTileUpdateEvent += (pos, tile) =>
        {
            var collider = tile.GetCollider();
            physicsSystem.PlaceStaticCollider(pos, collider);
        };
    }

    public bool ExistsAt(Vec2Int pos)
        => physicsSystem.ExistsStaticCollider(pos);
}
