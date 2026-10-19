using System.Collections.Generic;
using System.Collections.Immutable;

namespace TowerProtect.Domain.Model.Entities;

public readonly record struct EntityListData(
    ImmutableArray<EntityData> Entities)
{
    public IEnumerable<EntityData> Unpack() => Entities;
}
