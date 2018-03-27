using UnityEngine;

namespace Game.Network.Login
{
    [System.Serializable]
    public class ServerInfoSetting
    {
        public static readonly string localUrl = Application.streamingAssetsPath + "/server_list.json";
        public static readonly string onLineUrl = "http://localhost/server_list.json";
    }
}