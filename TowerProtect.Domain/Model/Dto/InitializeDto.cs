using System;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Model.Dto;

public record InitializeDto(
    Guid PlayerGuid,
    TilemapData Tilemap);
