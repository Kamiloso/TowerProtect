using System.Collections.Generic;
using TowerProtect.Domain.Model.Entities;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Client.Entities.Model;

internal class SmoothEntity
{
    private const int BufferSize = 5;

    private readonly LinkedList<Vec2> _positions = [];
    private readonly LinkedList<double> _rotations = [];

    public SmoothEntity(EntityData initialEntity)
    {
        _positions.AddLast(initialEntity.Position);
        _rotations.AddLast(initialEntity.Rotation);
    }

    public void NextFrame(EntityData nextEntity)
    {
        
    }

    private record Transform(Vec2 Position, double Rotation);
}
