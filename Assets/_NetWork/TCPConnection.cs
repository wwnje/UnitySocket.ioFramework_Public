using BestHTTP.SocketIO;
using System;
using UniRx;
using UnityEngine;

public class TCPConnection : ITCPConnection
{
    SocketManager Manager;
    Subject<byte[]> subject;

    public IObservable<byte[]> ComingData()
    {
        return subject ?? new Subject<byte[]>();
    }

    public bool IsConnected()
    {
        return false;
    }

    public IObservable<Unit> Connect(string url)
    {
        // client not null
        return null;
    }

    public void Connect_2(string url)
    {
        // Change an option to show how it should be done
        SocketOptions options = new SocketOptions
        {
            AutoConnect = false
        };

        // Create the Socket.IO manager
        Manager = new SocketManager(new Uri(url), options);

        // The argument will be an Error object.
        Manager.Socket.On(SocketIOEventTypes.Error, (socket, packet, args) => Debug.LogError(string.Format("Error: {0}", args[0].ToString())));

        Manager.GetSocket("/nsp").On(SocketIOEventTypes.Connect, (socket, packet, arg) =>
        {
            Debug.LogWarning("Connected to /nsp");

            socket.Emit("testmsg", "Message from /nsp 'on connect'");
        });

        Manager.GetSocket("/nsp").On("nsp_message", (socket, packet, arg) =>
        {
            Debug.LogWarning("nsp_message: " + arg[0]);
        });

        // We set SocketOptions' AutoConnect to false, so we have to call it manually.
        Manager.Open();
    }

    public void Send(string eventName, string msg)
    {
        Manager.Socket.Emit(eventName, msg);
    }

    public void Bind(string eventName, BestHTTP.SocketIO.Events.SocketIOCallback callback)
    {
        Manager.Socket.On(eventName, callback);
    }

    public void Close()
    {
        Manager.Close();
    }
}
