using System;
using System.Diagnostics;

namespace TowerProtect.Domain.Tools;

public class Animated<T>(T initial,
    Stopwatch stopwatch,
    Func<T, long, T> animate)
{
    public T Animate() => animate(
        initial,
        stopwatch.ElapsedMilliseconds
    );
}
