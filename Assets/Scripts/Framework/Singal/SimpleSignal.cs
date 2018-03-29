using System;
using UniRx;

namespace Game.Framework
{
    public class SimpleSignal : IObservable<Unit>
    {
        private Subject<Unit> _subject = new Subject<Unit>();
        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void Fire()
        {
            _subject.OnNext(Unit.Default);
        }
    }

    public class SimpleSignal<T> : IObservable<T>
    {
        private Subject<T> _subject = new Subject<T>();
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void Fire(T param)
        {
            _subject.OnNext(param);
        }
    }
}
