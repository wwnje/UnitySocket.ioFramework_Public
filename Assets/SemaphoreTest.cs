using System.Threading;
using UnityEngine;

public class SemaphoreTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Test();
    }


    // 初始信号量计数为0，最大计数为10
    public static Semaphore semaphore = new Semaphore(0, 10);
    public static int time = 0;

    void Test()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread test = new Thread(new ParameterizedThreadStart(TestMethod));

            // 开始线程，并传递参数
            test.Start(i);
        }

        // 等待1秒让所有线程开始并阻塞在信号量上
        Thread.Sleep(500);

        // 信号量计数加4
        // 最后可以看到输出结果次数为4次
        semaphore.Release(4);
    }

    public static void TestMethod(object number)
    {
        // 设置一个时间间隔让输出有顺序
        int span = Interlocked.Add(ref time, 100);
        Thread.Sleep(1000 + span);

        //信号量计数减1
        semaphore.WaitOne();

        Debug.LogFormat("Thread {0} run ", number);
    }
}
