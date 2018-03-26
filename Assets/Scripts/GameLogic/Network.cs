using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Network : INetwork, INetworkConnection
{
    Zenject.IFactory<ISocketIOConnection> connectionFactory = null;
    ISocketIOConnection conn = null;
    Subject<Dictionary<string, object>> subject = new Subject<Dictionary<string, object>>();

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

    public IObservable<Dictionary<string, object>> Receive()
    {
        return subject;
    }

    public void OnData(Dictionary<string, object> data)
    {
        subject.OnNext(data);
    }

    void OnDataException()
    {
        DisConnect();
    }

    public void Send(string eventName, string msg)
    {
        conn.Send(eventName, msg);
    }

    public void Bind(string eventName)
    {
        conn.Bind(eventName);
    }
}
