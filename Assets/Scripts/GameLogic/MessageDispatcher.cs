using System;
using System.Collections.Generic;
using UniRx;

public class MessageDispatcher : IMessageDispatcher
{
    Dictionary<Type, object> subjects = new Dictionary<Type, object>();

    public void Public(object message, Type type)
    {
        object subject = null;
        if (subjects.TryGetValue(type, out subject))
        {
            (subject as IMessagePublisher).Public(message);
        }
    }

    public IObservable<T> Receive<T>() where T : class
    {
        object subject = null;
        if (subjects.TryGetValue(typeof(T), out subject))
        {
            return subject as IObservable<T>;
        }

        subject = new MessageSubject<T>();
        subjects.Add(typeof(T), subject);
        return subject as IObservable<T>;
    }
}