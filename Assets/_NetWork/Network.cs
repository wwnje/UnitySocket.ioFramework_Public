using System;
using UniRx;
using UnityEngine;

public class Network : INetwork, INetworkConnection
{
    Zenject.IFactory<ISocketIOConnection> connectionFactory = null;
    ISocketIOConnection conn = null;

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
            Debug.Log("Conn Suc");
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

    public void OnData(byte[] data)
    {
        Debug.Log("OnData");
    }

    void OnDataException()
    {
        DisConnect();
    }

    public IObservable<T> Receive<T>() where T : class
    {
        throw new System.NotImplementedException();
    }

    public byte[] Receive()
    {
        return null;
    }

    public void Send(string eventName, string msg)
    {
        conn.Send(eventName, msg);
    }

    public void Bind(string eventName, BestHTTP.SocketIO.Events.SocketIOCallback callback)
    {
        conn.Bind(eventName, callback);
    }
}
