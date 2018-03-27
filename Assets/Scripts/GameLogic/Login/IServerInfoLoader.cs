using UniRx;

namespace Game.Login.Internal
{
    public interface IServerInfoLoader
    {
        IObservable<ServerInfo> Load();
    }
}