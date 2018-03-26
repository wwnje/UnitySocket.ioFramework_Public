public class ConnectionFactory : Zenject.IFactory<ISocketIOConnection>
{
    public ISocketIOConnection Create()
    {
        return new SocketIOConnection();
    }
}
