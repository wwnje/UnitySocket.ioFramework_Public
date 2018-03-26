using BestHTTP.SocketIO;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SocketIOConnection : ISocketIOConnection
{
    SocketManager Manager;
    Subject<Dictionary<string, object>> subject = new Subject<Dictionary<string, object>>();
    List<string> eventNameLst = new List<string>();

    public IObservable<Dictionary<string, object>> ComingData()
    {
        return subject;
    }

    public void Close()
    {
        if (Manager == null) return;

        Manager.Close();

        subject.Dispose();

        eventNameLst.Clear();
    }

    public bool IsConnected()
    {
        return Manager != null ? (Manager.State == SocketManager.States.Open) : false;
    }

    public IObservable<Unit> Connect(string url)
    {
        if (Manager != null)
        {
            return ConnectInvalid();
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

                Manager.Open();
                return Disposable.Empty;
            });
    }

    public void Send(string eventName, string msg)
    {
        Manager.Socket.Emit(eventName, msg);
    }

    public void Bind(string eventName)
    {
        if (eventNameLst.Contains(eventName)) return;

        eventNameLst.Add(eventName);

        Manager.Socket.On(eventName,
                    (socket, packet, args) =>
                    {
                        var data = args[0] as Dictionary<string, object>;
                        subject.OnNext(data);
                    });
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
