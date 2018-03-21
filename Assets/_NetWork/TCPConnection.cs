using UniRx;

public class TCPConnection : ITCPConnection
{
    public void Close()
    {
        throw new System.NotImplementedException();
    }

    public IObservable<byte[]> ComingData()
    {
        return null;
    }

    public bool IsConnected()
    {
        return false;
    }

    public void Send(byte[] data)
    {
        throw new System.NotImplementedException();
    }

    IObservable<Unit> ITCPConnection.Connect(string host, int port)
    {
        return null;
    }
}
