using AutoInterfaceAttributes;
using TowerProtect.Domain.Game.Environment;
using TowerProtect.Domain.Game.Tiles;

namespace TowerProtect.App.Client;

[AutoInterface]
internal class ClientState : IClientState
{
    public ClientTilemap Tilemap { get; } = new(0);
    public ClientBorders Borders { get; } = new(0);

    private bool _started;

    public void Start(int range)
    {
        if (_started) return;
        _started = true;

        Tilemap.Recreate(range);
        Borders.Recreate(range);
    }
}
