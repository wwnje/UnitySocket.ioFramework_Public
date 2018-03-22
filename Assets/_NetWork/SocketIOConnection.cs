using BestHTTP.SocketIO;
using System;
using UniRx;
using UnityEngine;

public class SocketIOConnection : ISocketIOConnection
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
        if (Manager != null)
        {
            ConnectInvalid();
        }

        return Observable.Create<Unit>(observer =>
            {
                SocketOptions options = new SocketOptions
                {
                    AutoConnect = false
                };

                Manager = new SocketManager(new Uri(url), options);

                Manager.Socket.On(SocketIOEventTypes.Error,
                    (socket, packet, args) =>
                    {
                        Debug.LogError(string.Format("Error: {0}", args[0].ToString()));
                        observer.OnError(new InvalidOperationException());
                    });

                Manager.Socket.On(SocketIOEventTypes.Connect,
                    (socket, packet, args) =>
                    {
                        observer.OnNext(Unit.Default);
                    });

                Manager.GetSocket("/nsp").On(SocketIOEventTypes.Connect, (socket, packet, arg) =>
                {
                    Debug.LogWarning("Connected to /nsp");
                    socket.Emit("testmsg", "Message from /nsp 'on connect'");
                });

                Manager.GetSocket("/nsp").On("nsp_message", (socket, packet, arg) =>
                {
                    Debug.LogWarning("nsp_message: " + arg[0]);
                });

                Manager.Open();
                return Disposable.Empty;
            });
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

    IObservable<Unit> ConnectInvalid()
    {
        return Observable.Create<Unit>(observer =>
        {
            observer.OnError(new InvalidOperationException());
            return Disposable.Empty;
        });
    }
}
