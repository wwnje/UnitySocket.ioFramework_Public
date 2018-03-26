using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ReceiveMessage
{
    public Dictionary<string, object> data;
}

public class NetIns
{
    private static NetIns _ins;
    public static NetIns Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new NetIns();
            }
            return _ins;
        }
    }

    public delegate void MessageHandler<T>(T msg);
    interface IHandler
    {
        void Invoke(object msg);
    }

    class Handler<T> : IHandler where T : GameMessage
    {
        public MessageHandler<T> handle;
        public void Invoke(object msg)
        {
            handle.Invoke(msg as T);
        }
    }

    Dictionary<Type, object> msgHandlers = new Dictionary<Type, object>();

    Network _network = null;

    const string LOCAL_URL = "http://localhost:3000/socket.io/";


    public void Connect()
    {
        _network = new Network(new ConnectionFactory());
        _network.Connect(LOCAL_URL);

        _network.Receive<ReceiveMessage>().Subscribe(OnReceive,
            error =>
        {
            Debug.LogException(error);
        });
    }

    public void Send(string msg, string eventName = "message")
    {
        GameMessage netMsg = new GameMessage
        {
            socketEventName = eventName,
            msg = msg,
        };

        _network.Send(netMsg);
    }

    public bool AddHandler<T>(MessageHandler<T> handler, string socketEventName = "message") where T : GameMessage
    {
        Type type = typeof(T);

        if (msgHandlers.ContainsKey(type))
        {
            Debug.LogError("多处监听");
            return false;
        }

        var holder = new Handler<T>
        {
            handle = handler
        };

        msgHandlers[type] = holder;
        RegisterMsg(type);
        _network.Bind(socketEventName);
        return true;
    }

    void OnReceive (ReceiveMessage msg)
    {
        var data = msg.data;

        Type type = typeDic[data["type"] as String];
        object realmsg = JsonUtility.FromJson(data["json"] as String, type);

        var handler= msgHandlers[type] as IHandler;

        try
        {
            handler.Invoke(realmsg);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    Dictionary<string, Type> typeDic = new Dictionary<string, Type>();

    void RegisterMsg(Type type)
    {
        typeDic[type.ToString()] = type;
    }

    void OnDestroy()
    {
        // disconnnect
    }
}
