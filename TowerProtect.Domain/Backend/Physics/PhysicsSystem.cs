using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.Domain.Config;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Physics;

[AutoInterface]
internal class PhysicsSystem(PhysicsConfig cfg) : IPhysicsSystem
{
    private readonly Dictionary<Vec2Int, StaticCollider> _staticColliders = [];
    private readonly List<StaticCollider> _reusableBuffer = [];

    public bool ExistsStaticCollider(Vec2Int pos)
        => _staticColliders.ContainsKey(pos);

    public void PlaceStaticCollider(Vec2Int pos, Vec2? size)
    {
        if (size.HasValue)
        {
            _staticColliders[pos] = new StaticCollider
            {
                Position = pos,
                Size = size.Value
            };
        }
        else
        {
            _staticColliders.Remove(pos);
        }
    }

    public void AttachDynamicCollider(DynamicCollider collider)
        => collider.OnUpdateEvent += UpdateDynamicCollider;

    public void DetachDynamicCollider(DynamicCollider collider)
        => collider.OnUpdateEvent -= UpdateDynamicCollider;

    public void UpdateDynamicCollider(DynamicCollider collider, double deltaTime)
    {
        Vec2Int center = collider.Position.Round();
        
        _reusableBuffer.Clear();

        Area.Iterate(cfg.IncludeRange, rPos =>
        {
            Vec2Int pos = center + rPos;

            if (_staticColliders.TryGetValue(pos, out var collider))
                _reusableBuffer.Add(collider);
        });

        collider.Move(deltaTime, _reusableBuffer);
    }
}
