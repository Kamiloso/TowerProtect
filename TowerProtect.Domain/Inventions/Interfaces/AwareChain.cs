using System.Collections.Generic;

namespace TowerProtect.Domain.Inventions.Interfaces;

public interface IAwareChain : IStartAware, IUpdateAware
{
    protected abstract IEnumerable<object> Children { get; }

    void IStartAware.Start()
    {
        foreach (var child in Children)
        {
            if (child is IStartAware aware)
                aware.Start();
        }
    }

    void IUpdateAware.Update(double deltaTime)
    {
        foreach (var child in Children)
        {
            if (child is IUpdateAware aware)
                aware.Update(deltaTime);
        }
    }
}
