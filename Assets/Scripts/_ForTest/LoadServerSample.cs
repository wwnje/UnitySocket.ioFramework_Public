using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadServerSample : MonoBehaviour
{
    ServerInfo info = null;
    LoginIns login;

    // Use this for initialization
    void Start()
    {
        login = LoginIns.Ins;
        login.LoadServerInfo();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh"))
        {
            login.LoadServerInfo();
            info = login.GetServerInfo();
            Debug.Log(info != null ? info.announcement : "null");
        }
    }
}
