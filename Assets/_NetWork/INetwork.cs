using UniRx;
public interface INetwork
{
    IObservable<T> Receive<T>() where T : class;
    byte[] Receive();
    void Send<T>(T pack) where T : IPacket;
}

public interface IPacket
{
    byte[] ToBytes();
}
