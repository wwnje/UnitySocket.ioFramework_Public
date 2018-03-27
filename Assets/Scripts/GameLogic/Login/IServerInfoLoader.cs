using UniRx;
namespace Game.Network.Login
{
    public interface IServerInfoLoader
    {
        IObservable<ServerInfo> Load();
    }
}