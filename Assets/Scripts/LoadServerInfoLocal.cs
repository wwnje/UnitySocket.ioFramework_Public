using System.Collections;
using System.IO;
using UnityEngine;

public class LoadServerInfoLocal : ILoadServerInfo
{
    ServerInfo serverInfo = null;

    public IEnumerator LoadServerInfo()
    {
        string filePath = Application.streamingAssetsPath + "/server_list.json";
        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            serverInfo = JsonUtility.FromJson<ServerInfo>(dataAsJson);
        }

        yield return null;
    }

    public ServerInfo GetServerInfo()
    {
        return serverInfo;
    }
}