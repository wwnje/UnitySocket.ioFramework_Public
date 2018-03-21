using System.Collections;
public interface INetworkConnection
{
    void Connect(string host, int port);
    void DisConnect();
}
