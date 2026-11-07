using AutoInterfaceAttributes;
using System;
using System.Collections.Generic;
using TowerProtect.Domain.Backend.Entities.Input;
using TowerProtect.Domain.Model.Unions;

namespace TowerProtect.Domain.Backend.Entities.Systems;

[AutoInterface]
internal class PlayerSystem(ISpawnSystem spawnSystem) : IPlayerSystem
{
    private readonly Dictionary<long, ManualController> _controllers = [];

    public bool Exists(long client)
        => _controllers.ContainsKey(client);

    public bool TrySpawn(long client, out Guid guid)
    {
        if (_controllers.ContainsKey(client))
        {
			guid = Guid.Empty;
			return false;
		}

        var controller = new ManualController();
        _controllers.Add(client, controller);

        guid = spawnSystem.SpawnPlayer(controller);

        return true;
    }

    public void AcceptInput(long client, InputUnion inputUnion)
    {
        if (!_controllers.TryGetValue(client, out var controller))
            return;

        controller.AcceptInput(inputUnion);
    }
}
