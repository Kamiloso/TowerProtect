namespace TowerProtect.App.Networking.Client;

public interface IClientFrontPipe
{
    void Send(string message);
    bool TryDequeue(out string message);
    bool IsDead();
}
