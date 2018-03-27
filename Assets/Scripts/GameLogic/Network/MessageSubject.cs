using System;
using UniRx;

public class MessageSubject<T> : IObservable<T>, IMessagePublisher where T : class
{
    Subject<T> subject = new Subject<T>();

    public IDisposable Subscribe(IObserver<T> observer)
    {
        return subject.Subscribe(observer);
    }

    public void Public(object message)
    {
        T msg = message as T;
        if (msg != null)
        {
            subject.OnNext(msg);
        }
    }
}
