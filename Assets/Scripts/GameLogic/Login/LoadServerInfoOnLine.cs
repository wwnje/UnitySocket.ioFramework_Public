using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Network.Login
{
    public class ServerInfoOnLineLoader : IServerInfoLoader
    {
        public IObservable<ServerInfo> Load()
        {
            return Observable.FromCoroutine<ServerInfo>((observer, cancellationToken) => DoLoad(observer));
        }

        public IEnumerator DoLoad(IObserver<ServerInfo> observer)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(ServerInfoSetting.onLineUrl))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    ServerInfo serverInfo = JsonUtility.FromJson<ServerInfo>(www.downloadHandler.text);
                    observer.OnNext(serverInfo);
                }
            }
        }
    }
}