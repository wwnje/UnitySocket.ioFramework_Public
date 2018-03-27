using System;
using UniRx;

public interface IMessageDispatcher
{
    IObservable<T> Receive<T>() where T : class;
    void Public(object message, Type type);
}
