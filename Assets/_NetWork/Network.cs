using UniRx;
using UnityEngine;

public class Network : INetwork, INetworkConnection
{
    Zenject.IFactory<ITCPConnection> connectionFactory = null;
    ITCPConnection conn = null;

    public Network(Zenject.IFactory<ITCPConnection> factory)
    {
        connectionFactory = factory;
    }

    public void Connect(string url)
    {
        if (conn != null)
        {
            Debug.LogError("conn is not null");
            return;
        }

        conn = connectionFactory.Create();
        conn.ComingData().Subscribe(OnData, OnDataException);

        //conn.Connect(url).Subscribe(_ =>
        //{
        //    Debug.Log("Conn Suc");
        //}, error =>
        //{
        //    Debug.Log("Conn error" + error);
        //    DisConnect();
        //}
        //);
        conn.Connect_2(url);
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
        //
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
