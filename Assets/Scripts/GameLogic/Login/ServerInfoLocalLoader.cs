using System.IO;
using UniRx;
using UnityEngine;

namespace Game.Login.Internal
{
    public class ServerInfoLocalLoader : IServerInfoLoader
    {
        ServerInfoSetting _setting = null;

        public ServerInfoLocalLoader(ServerInfoSetting setting)
        {
            _setting = setting;
        }

        public IObservable<ServerInfo> Load()
        {
            return Observable.Create<ServerInfo>(observer =>
            {
                string url = Application.streamingAssetsPath + _setting.LocalUrl;
                if (!string.IsNullOrEmpty(url))
                {
                    string dataAsJson = File.ReadAllText(url);
                    ServerInfo serverInfo = JsonUtility.FromJson<ServerInfo>(dataAsJson);
                    observer.OnNext(serverInfo);
                }
                return Disposable.Empty;
            });
        }
    }
}
