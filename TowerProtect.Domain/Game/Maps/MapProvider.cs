using AutoInterfaceAttributes;
using System;
using TowerProtect.Domain.Game.Maps.Specific;

namespace TowerProtect.Domain.Game.Maps;

[AutoInterface]
internal class MapProvider : IMapProvider
{
    private readonly Random _random = new();

    private static readonly Map[] Maps = [
        new PlaceholderMap()
    ];

    public Map GetMap(int? index)
    {
        index ??= _random.Next();
        index %= Maps.Length;

        return Maps[index.Value];
    }
}
