using BestHTTP.SocketIO.Events;
using UniRx;

public interface ISocketIOConnection
{
    bool IsConnected();
    IObservable<Unit> Connect(string url);
    void Close();
    IObservable<byte[]> ComingData();
    void Send(string eventName, string msg);

    void Bind(string eventName, SocketIOCallback callback);
}