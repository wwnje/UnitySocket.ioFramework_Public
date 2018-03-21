using UniRx;

public interface ITCPConnection
{
    bool IsConnected();
    IObservable<Unit> Connect(string host, int port);
    void Close();
    IObservable<byte[]> ComingData();
    void Send(byte[] data);

    void Connect_2(string host, int port);
}