using AutoInterfaceAttributes;
using System.Collections.Generic;
using TowerProtect.Domain.Inventions.Interfaces;
using TowerProtect.Domain.Inventions.Tools;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Backend.Physics;

partial interface IPhysicsSystem : IUpdateAware;

[AutoInterface]
internal class PhysicsSystem(int includeRange) : IPhysicsSystem
{
    private readonly Dictionary<Vec2Int, StaticCollider> _staticColliders = [];
    private readonly HashSet<DynamicCollider> _dynamicColliders = new(ReferenceEqualityComparer.Instance);
    private readonly List<StaticCollider> _reusableBuffer = [];

    public void Update(double deltaTime)
    {
        foreach (var collider in _dynamicColliders)
        {
            Vec2Int center = collider.Position.Round();
            FillReusableBuffer(center);

            collider.Move(deltaTime, _reusableBuffer);
        }
    }

    private void FillReusableBuffer(Vec2Int center)
    {
        _reusableBuffer.Clear();

        Area.Iterate(includeRange, rPos =>
        {
            Vec2Int pos = center + rPos;

            if (_staticColliders.TryGetValue(pos, out var collider))
                _reusableBuffer.Add(collider);
        });
    }

    public void AddDynamicCollider(DynamicCollider collider)
        => _dynamicColliders.Add(collider);

    public bool RemoveDynamicCollider(DynamicCollider collider)
        => _dynamicColliders.Remove(collider);

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
}
