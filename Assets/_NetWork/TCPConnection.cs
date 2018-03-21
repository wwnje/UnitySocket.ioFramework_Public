using BestHTTP.SocketIO;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TCPConnection : ITCPConnection
{
    SocketManager Manager;
    Subject<byte[]> subject;

    public void Close()
    {
        throw new System.NotImplementedException();
    }

    public IObservable<byte[]> ComingData()
    {
        return subject ?? new Subject<byte[]>();
    }

    public bool IsConnected()
    {
        return false;
    }

    public void Send(byte[] data)
    {
        throw new System.NotImplementedException();
    }

    public IObservable<Unit> Connect(string host, int port)
    {
        // client not null
        return null;
    }

    public void Connect_2(string host, int port)
    {
       
    }
}
