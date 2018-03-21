using UnityEngine;
using UniRx;
using System.Collections;
using System;

public class UniRxLearn : MonoBehaviour
{
    Subject<IEnumerator> _subIEnumerator = null;
    BoolReactiveProperty _bool = new BoolReactiveProperty(true);
    Action _action;
    Subject<byte[]> _onData;

    CompositeDisposable _disposable = new CompositeDisposable();
    // Use this for initialization
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
        ComingData()
            .Subscribe(_ => OnData(_))
            .AddTo(_disposable);
        if (_onData != null)
        {
            byte[] bytes = BitConverter.GetBytes(1);
            _onData.OnNext(bytes);
        }
    }

    IEnumerator Test_Subject()
    {
        Debug.Log("Test_Subject");
        yield return null;
    }

    IObservable<byte[]> ComingData()
    {
        return _onData ?? (_onData = new Subject<byte[]>());
    }

    void OnData(byte[] data)
    {
        Debug.Log("OnData:" + data.Length);
        _disposable.Clear();
    }
}
