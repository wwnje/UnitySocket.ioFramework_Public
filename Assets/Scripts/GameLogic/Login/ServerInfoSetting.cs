using UnityEngine;

namespace Game.Login
{
    [System.Serializable]
    public class ServerInfoSetting
    {
        public string LocalUrl = "/server_list.json";
        public string OnLineUrl = "http://localhost/server_list.json";
    }
}