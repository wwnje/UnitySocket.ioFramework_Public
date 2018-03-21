public class ConnectionFactory : Zenject.IFactory<ITCPConnection>
{
    public ITCPConnection Create()
    {
        return new TCPConnection();
    }
}
