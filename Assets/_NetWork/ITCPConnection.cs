using BestHTTP.SocketIO.Events;
using UniRx;

public interface ITCPConnection
{
    bool IsConnected();
    IObservable<Unit> Connect(string url);
    void Close();
    IObservable<byte[]> ComingData();
    void Send(string eventName, string msg);

    void Connect_2(string url);
    void Bind(string eventName, SocketIOCallback callback);
}