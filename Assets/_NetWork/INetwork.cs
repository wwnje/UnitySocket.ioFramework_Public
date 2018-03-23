using System.Collections.Generic;
using UniRx;
public interface INetwork
{
    IObservable<Dictionary<string, object>> Receive();
    void Send(string eventName, string msg);
}
