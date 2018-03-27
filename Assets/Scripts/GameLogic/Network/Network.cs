using System;
using UniRx;
using UnityEngine;

public class Network : INetwork, INetworkConnection
{
    Zenject.IFactory<ISocketIOConnection> connectionFactory = null;
    ISocketIOConnection conn = null;
    IMessageDispatcher dispatcher = null;

    public Network(Zenject.IFactory<ISocketIOConnection> factory)
    {
        connectionFactory = factory;
    }

    public void Connect(string url)
    {
        if (conn != null)
        {
            throw new InvalidOperationException();
        }

        dispatcher = new MessageDispatcher();

        conn = connectionFactory.Create();
        conn.ComingData().Subscribe(OnData, OnDataException);

        conn.Connect(url).Subscribe(_ =>
        {
            Debug.Log("Connect Success.");
        }, error =>
        {
            Debug.LogException(error);
            DisConnect();
        }
        );
    }

    public void DisConnect()
    {
        if (conn != null)
        {
            conn.Close();
            conn = null;
        }
    }

    public void OnData(object data)
    {
        dispatcher.Public(data as ReceiveMessage, typeof(ReceiveMessage));
    }

    void OnDataException()
    {
        DisConnect();
    }

    public IObservable<T> Receive<T>() where T : class
    {
        return dispatcher.Receive<T>();
    }

    public void Send<T>(T pack) where T : GameMessage
    {
        conn.Send(pack.socketEventName, pack.msg);
    }

    public void Bind(string eventName)
    {
        conn.Bind(eventName);
    }
}
