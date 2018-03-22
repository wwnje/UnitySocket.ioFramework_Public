using UnityEngine;
using UniRx;
using System.Collections;
using System;
using System.Net.Sockets;

public class UniRxLearn : MonoBehaviour
{
    Subject<IEnumerator> _subIEnumerator = null;
    BoolReactiveProperty _bool = new BoolReactiveProperty(true);
    Action _action;
    Subject<byte[]> _onOb;

    CompositeDisposable _disposable = new CompositeDisposable();
    // Use this for initialization

    TcpClient client;

    void Start()
    {
        //Subject
        _subIEnumerator = new Subject<IEnumerator>().AddTo(this);
        _subIEnumerator
            .Subscribe(_ => StartCoroutine(_))
            .AddTo(this);
        _subIEnumerator.OnNext(Test_Subject());

        //BoolReactiveProperty
        _bool.Subscribe(_ =>
        {
            Debug.Log("IsBool:" + _); ;
        })
        .AddTo(this);
        _bool.Value = false;

        //Observable
        Observable.FromEvent(_ => _action += _, _ => _action -= _)
            .Where(_ => true == _bool.Value)
            .Subscribe(_ =>
           {
               Debug.Log("action: " + _);
           })
            .AddTo(this);
        _bool.Value = true;
        _action();

        //IObservable<byte[]>
        ObTest()
            .Subscribe(_ => OnData(_))
            .AddTo(_disposable);

        if (_onOb != null)
        {
            byte[] bytes = BitConverter.GetBytes(1);
            _onOb.OnNext(bytes);
        }

        //ConnectEnter
        ConnectEnter();
    }

    IEnumerator Test_Subject()
    {
        Debug.Log("Test_Subject");
        yield return null;
    }

    IObservable<byte[]> ObTest()
    {
        return _onOb ?? (_onOb = new Subject<byte[]>());
    }

    void OnData(byte[] data)
    {
        Debug.Log("OnData:" + data.Length);
        _disposable.Clear();
    }

    public void ConnectEnter()
    {
        Connect("127.0.0.1", 80).Subscribe(_ =>
        {
            Debug.Log("ConSuc");
        }, error =>
        {
            Debug.LogError("error");
            Debug.LogException(error);
        });
    }

    public IObservable<Unit> Connect(string host, int port)
    {
        // client not null
        // observer     观察者
        // observable   观察
        return Observable.Create<Unit>(observer =>
        {
            Debug.Log("new TcpClient.");
            client = new TcpClient();
            var observable = Observable.FromAsyncPattern((callback, obj) =>
            {
                Debug.Log("BeginConnect");
                return client.BeginConnect(host, port, callback, obj);
            }, asyncResult =>
            {
                Debug.LogError("EndConnect");
                client.EndConnect(asyncResult);
            })();

            return observable.Timeout(TimeSpan.FromSeconds(10))
            .DoOnCompleted(() =>
            {
                Debug.Log("StartSendCoroutine()");
                Debug.Log("StartREcvCoroutine()");
            }).Subscribe(observer);
        });
    }
}
