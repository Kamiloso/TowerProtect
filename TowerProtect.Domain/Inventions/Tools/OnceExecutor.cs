using System;

namespace TowerProtect.Domain.Inventions.Tools;

public class OnceExecutor
{
    private bool _done = false;

    public void WhenFirstRun(Action action)
    {
        if (_done) return;
        _done = true;

        action();
    }
}
