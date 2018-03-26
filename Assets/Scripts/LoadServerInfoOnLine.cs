using System.Collections;
using UnityEngine;

public class LoadServerInfoOnLine : ILoadServerInfo
{
    ServerInfo serverInfo = null;

    public IEnumerator LoadServerInfo()
    {
        string url = "http://localhost/server_list.json";
        WWW www = new WWW(url);

        yield return www;

        if (string.IsNullOrEmpty(www.error) && www.isDone)
        {
            serverInfo = JsonUtility.FromJson<ServerInfo>(www.text);
        }

        www.Dispose();
    }

    public ServerInfo GetServerInfo()
    {
        return serverInfo;
    }
}