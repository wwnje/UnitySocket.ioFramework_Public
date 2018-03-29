using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ManualResetEventTest : MonoBehaviour
{
    ManualResetEvent _event = new ManualResetEvent(false);

    // Use this for initialization
    void Start()
    {
        Thread t = new Thread(Do);
        t.Start();

        StartCoroutine(DoWork());

        Debug.Log("hello");

        _event.Set();
    }

    void Do()
    {
        _event.WaitOne();
        Debug.Log("Do");
    }

    IEnumerator DoWork()
    {
        yield return null;
        Debug.Log("DoWork");
    }
}
