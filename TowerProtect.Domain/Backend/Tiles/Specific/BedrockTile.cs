using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Tiles.Specific;

public sealed class BedrockTile : Tile
{
    public override TileType Type => TileType.Bedrock;
}
