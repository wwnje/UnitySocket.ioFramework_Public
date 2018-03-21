using UniRx;
public interface INetwork
{
    IObservable<T> Receive<T>() where T : class;
    byte[] Receive();
    void Send(string eventName, string msg);
}
