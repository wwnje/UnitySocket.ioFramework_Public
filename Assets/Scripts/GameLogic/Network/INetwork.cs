using UniRx;

public interface INetwork
{
    IObservable<T> Receive<T>() where T : class;
    void Send<T>(T pack) where T: GameMessage;
}

public class GameMessage
{
    public string socketEventName;
    public string msg;
}
