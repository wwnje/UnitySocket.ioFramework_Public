using BestHTTP.SocketIO.Events;
using NUnit.Framework;
using UniRx;
using Zenject;

[TestFixture]
public class NetWorkTest : ZenjectUnitTestFixture
{
    public class MockNetwork : INetwork, INetworkConnection
    {
        public void Connect(string url)
        {
            throw new System.NotImplementedException();
        }

        public void DisConnect()
        {
            throw new System.NotImplementedException();
        }

        public IObservable<T> Receive<T>() where T : class
        {
            throw new System.NotImplementedException();
        }

        public byte[] Receive()
        {
            throw new System.NotImplementedException();
        }

        public void Send(string eventName, string msg)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MockSocketIOConnection : ISocketIOConnection
    {
        public void Bind(string eventName, SocketIOCallback callback)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public IObservable<byte[]> ComingData()
        {
            throw new System.NotImplementedException();
        }

        public IObservable<Unit> Connect(string url)
        {
            throw new System.NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new System.NotImplementedException();
        }
        public void Send(string eventName, string msg)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MockNetworkFactory : Zenject.IFactory<ISocketIOConnection>
    {
        public ISocketIOConnection Create()
        {
            return new MockSocketIOConnection();
        }
    }

    [Inject]
    INetwork network = null;

    [Inject]
    INetworkConnection network_conn = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Zenject.IFactory<ISocketIOConnection>>().To<MockNetworkFactory>().AsSingle();
        Container.Bind<INetwork>().To<MockNetwork>().AsSingle();
        Container.Bind<INetworkConnection>().To<MockNetwork>().AsSingle();
        Container.Inject(this);
    }

    [Test]
    public void Run_Test()
    {
        //Login
        network_conn.Connect("");

        byte[] data = network.Receive();
        Assert.AreEqual(null, data);
    }
}
