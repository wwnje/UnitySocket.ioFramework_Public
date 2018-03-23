using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class NetMgr
{
    private static NetMgr _ins;
    public static NetMgr Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new NetMgr();
            }
            return _ins;
        }
    }
    public class EventSlot
    {
        public string _onName = "";
        public EventCallback _callback = null;
    }

    public delegate void EventCallback(object arg);

    NetEvent _event = new NetEvent();
    Network _network = null;

    const string LOCAL_URL = "http://localhost:3000/socket.io/";

    public void Connect()
    {
        _network = new Network(new ConnectionFactory());
        _network.Connect(LOCAL_URL);

        _network.Receive().Subscribe(OnReceive,
            error =>
        {
            Debug.LogException(error);
        });
    }

    public void Send(string msg, string eventName = "message")
    {
        _network.Send(eventName, msg);
    }

    public void AddHandler(string eventname, EventCallback callback, string socketEventName = "message")
    {
        var slot = _event.ListenNetEvent(eventname, callback, socketEventName);
        _network.Bind(socketEventName);
    }

    void OnReceive(Dictionary<string, object> data)
    {
        var type = data["type"] as string;
        var json = data["json"] as string;

        EventSlot call = _event.GetEvent(type);
        if (call != null)
        {
            call._callback.Invoke(json);
        }
    }

    void OnDestroy()
    {
        // disconnnect
    }

    public class NetEvent
    {
        Dictionary<string, EventSlot> _EventDic = new Dictionary<string, EventSlot>();

        public EventSlot ListenNetEvent(string name, EventCallback callback, string socketOnMsg = "message")
        {
            if (!_EventDic.ContainsKey(name))
            {
                _EventDic[name] = new EventSlot
                {
                    _onName = socketOnMsg,
                    _callback = callback,
                };
            }

            return _EventDic[name];
        }

        public EventSlot GetEvent(string name)
        {
            return _EventDic.ContainsKey(name) ? _EventDic[name] : null;
        }
    }
}
