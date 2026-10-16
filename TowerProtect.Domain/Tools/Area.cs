using System;
using TowerProtect.Domain.Model.Vectors;

namespace TowerProtect.Domain.Tools;

public class Area<T>(int range) where T : class
{
    public int Range { get; } = range;
    public int Size { get; } = 2 * range + 1;
    public int Capacity => Size * Size;

    private readonly T[,] _data = new T[2 * range + 1, 2 * range + 1];

    public Area(int range, out int size) : this(range)
    {
        size = Size;
    }

    public T this[Vec2Int pos]
    {
        get => _data[pos.x + Range, pos.y + Range];
        set => _data[pos.x + Range, pos.y + Range] = value;
    }

    public bool Contains(Vec2Int pos)
    {
        return Math.Abs(pos.x) <= Range && Math.Abs(pos.y) <= Range;
    }

    public Area<T> Iterate(Action<Vec2Int> action)
    {
        for (int x = -Range; x <= Range; x++)
            for (int y = -Range; y <= Range; y++)
            {
                action(new(x, y));
            }

        return this;
    }

    public Area<T> Populate(Func<Vec2Int, T> factory)
    {
        return Iterate(pos => this[pos] = factory(pos));
    }
}
