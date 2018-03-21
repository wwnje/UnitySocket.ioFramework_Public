using NUnit.Framework;
using Zenject;

[TestFixture]
public class NetWorkTest : ZenjectUnitTestFixture
{
    public class MockNetworkFactory : Zenject.IFactory<ITCPConnection>
    {
        public ITCPConnection Create()
        {
            return new TCPConnection();
        }
    }

    [Inject]
    INetwork network = null;

    [Inject]
    INetworkConnection network_conn = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Zenject.IFactory<ITCPConnection>>().To<MockNetworkFactory>().AsSingle();

        Container.Bind<INetwork>().To<Network>().AsSingle();
        Container.Bind<INetworkConnection>().To<Network>().AsSingle();
        Container.Inject(this);
    }

    [Test]
    public void Run_Test()
    {
        network_conn.Connect("", 1);

        byte[] data = network.Receive();
        Assert.AreEqual(null, data);
    }
}
