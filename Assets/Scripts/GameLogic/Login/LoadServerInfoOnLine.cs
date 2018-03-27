using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Login.Internal
{
    public class ServerInfoOnLineLoader : IServerInfoLoader
    {
        ServerInfoSetting _setting = null;

        public ServerInfoOnLineLoader(ServerInfoSetting setting)
        {
            _setting = setting;
        }

        public IObservable<ServerInfo> Load()
        {
            return Observable.FromCoroutine<ServerInfo>((observer, cancellationToken) => DoLoad(observer));
        }

        public IEnumerator DoLoad(IObserver<ServerInfo> observer)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(_setting.OnLineUrl))
            {
                yield return www.SendWebRequest();

                if (www.isDone && string.IsNullOrEmpty(www.error))
                {
                    ServerInfo serverInfo = JsonUtility.FromJson<ServerInfo>(www.downloadHandler.text);
                    observer.OnNext(serverInfo);
                }
            }
        }
    }
}