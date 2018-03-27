using System.IO;
using UniRx;
using UnityEngine;

namespace Game.Network.Login
{
    public class ServerInfoLocalLoader : IServerInfoLoader
    {
        public IObservable<ServerInfo> Load()
        {
            return Observable.Create<ServerInfo>(observer =>
            {
                if (!string.IsNullOrEmpty(ServerInfoSetting.localUrl))
                {
                    string dataAsJson = File.ReadAllText(ServerInfoSetting.localUrl);
                    ServerInfo serverInfo = JsonUtility.FromJson<ServerInfo>(dataAsJson);
                    observer.OnNext(serverInfo);
                }
                return Disposable.Empty;
            });
        }
    }
}
