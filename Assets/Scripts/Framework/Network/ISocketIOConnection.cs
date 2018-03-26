using System.Collections.Generic;
using UniRx;

public interface ISocketIOConnection
{
    bool IsConnected();
    IObservable<Unit> Connect(string url);
    void Close();
    IObservable<object> ComingData();
    void Send(string eventName, string msg);

    void Bind(string eventName);
}