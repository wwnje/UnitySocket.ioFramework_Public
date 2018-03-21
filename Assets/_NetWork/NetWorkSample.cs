#if !BESTHTTP_DISABLE_SOCKETIO
using System;
using System.Collections.Generic;

using UnityEngine;
using BestHTTP.SocketIO;
using BestHTTP.Examples;

public class NetWorkSample : MonoBehaviour
{
    Zenject.IFactory<ITCPConnection> connectionFactory;
    Network network;

    const string url = "http://localhost:3000/socket.io/";

    private string userName = string.Empty;
    private string chatLog = string.Empty;

    bool isLogin = false;

    #region Unity Events

    private void Awake()
    {
        connectionFactory = new ConnectionFactory();
        network = new Network(connectionFactory);

    }

    void Start()
    {
        GUIHelper.ClientArea =
            new Rect(0, SampleSelector.statisticsHeight + 5, Screen.width, Screen.height - SampleSelector.statisticsHeight - 50);

        network.Connect(url);

        // Set up custom chat events
        network.Bind("login", OnLogin);
        network.Bind("user joined", OnUserJoined);
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
                    SetUserName();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        });
    }

    #endregion

    #region Chat Logic

    void SetUserName()
    {
        if (string.IsNullOrEmpty(userName))
            return;

        network.Send("add user", userName);
    }

    void AddParticipantsMessage(Dictionary<string, object> data)
    {
        int numUsers = Convert.ToInt32(data["numUsers"]);

        if (numUsers == 1)
            chatLog += "there's 1 participant\n";
        else
            chatLog += "there are " + numUsers + " participants\n";
    }

    #endregion

    #region Custom SocketIO Events

    void OnLogin(Socket socket, Packet packet, params object[] args)
    {
        isLogin = true;

        chatLog = "Welcome to Socket.IO Chat — \n";

        AddParticipantsMessage(args[0] as Dictionary<string, object>);
    }

    void OnUserJoined(Socket socket, Packet packet, params object[] args)
    {
        var data = args[0] as Dictionary<string, object>;

        var username = data["username"] as string;

        chatLog += string.Format("{0} joined\n", username);

        AddParticipantsMessage(data);
    }
    #endregion

    void OnDestroy()
    {
        network.DisConnect();
    }
}

#endif
