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

    public void Connect(string host, int port)
    {
        if (conn != null)
        {
            Debug.LogError("conn is not null");
            return;
        }

        conn = connectionFactory.Create();
        conn.ComingData().Subscribe(OnData, OnDataException);

        conn.Connect(host, port).Subscribe(_ =>
        {
            Debug.Log("Conn Suc");
        }, error =>
        {
            Debug.Log("Conn error" + error);
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

    public void Send<T>(T pack) where T : IPacket
    {
        throw new System.NotImplementedException();
    }
}
