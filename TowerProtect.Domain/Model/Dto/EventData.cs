namespace TowerProtect.Domain.Model.Dto;

public record EventData(
    string Command,
    string[] Args);
