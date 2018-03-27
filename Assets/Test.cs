using Game.Login;
using Game.Login.Internal;
using UnityEngine;
using UniRx;

public class Test : MonoBehaviour {

    ServerInfoSetting setting = new ServerInfoSetting();
    IServerInfoLoader loader_online = null;


    // Use this for initialization
    void Start () {
        loader_online = new ServerInfoOnLineLoader(setting);

        loader_online.Load()
       .Subscribe(info =>
       {
           Debug.Log(info.announcement);
       });
    }
}
