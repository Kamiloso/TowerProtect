using System;
using System.Diagnostics;

namespace TowerProtect.Domain.Inventions.Tools;

public class Animated<T>(Func<double, T> animate)
{
    public T Evaluate(Stopwatch stopwatch)
        => animate(stopwatch.ElapsedMilliseconds / 1000.0);
}
