#if !BESTHTTP_DISABLE_SOCKETIO
using UnityEngine;
using BestHTTP.Examples;

public class MsgLoginReturn : GameMessage
{
    public string str;
}

public class NetWorkSample : MonoBehaviour
{
    private string userName = string.Empty;
    private string chatLog = string.Empty;

    bool isLogin = false;

    NetIns _netMgr;


    private void Awake()
    {
        _netMgr = NetIns.Ins;
        _netMgr.Connect();
    }

    void Start()
    {
        GUIHelper.ClientArea =
            new Rect(0, SampleSelector.statisticsHeight + 5, Screen.width, Screen.height - SampleSelector.statisticsHeight - 50);

        //_netMgr.AddHandler(MsgLoginReturn, OnMessaged);
        _netMgr.AddHandler<MsgLoginReturn>(OnLogin, "login");
        //_netMgr.AddHandler("OnUserJoined", OnUserJoined, "user joined");
    }

    void OnGUI()
    {
        GUIHelper.DrawArea(GUIHelper.ClientArea, true, () =>
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label(chatLog, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            GUIHelper.DrawCenteredText(isLogin ? "Hello " + userName : "Login");

            if (!isLogin)
            {
                userName = GUILayout.TextField(userName);
                if (GUILayout.Button("Join"))
                {
                    SetUserName();
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        });
    }

    void SetUserName()
    {
        if (string.IsNullOrEmpty(userName))
        {
            return;
        }

        _netMgr.Send(userName, "add user");
    }

    void OnLogin(MsgLoginReturn data)
    {
        isLogin = true;
        chatLog = "Welcome to Socket.IO Chat — \n";
        Debug.Log("Login Return:" + data.str);
    }

    void OnUserJoined(object data)
    {
        Debug.Log(data);
        chatLog += string.Format("{0} joined\n", data);
    }

    void OnMessaged(object data)
    {
        Debug.Log("message:" + data);
    }
}
#endif
