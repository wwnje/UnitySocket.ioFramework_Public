using NUnit.Framework;
using Zenject;

[TestFixture]
public class NetWorkTest : ZenjectUnitTestFixture
{
    [Inject]
    INetwork network = null;

    [Inject]
    INetworkConnection network_conn = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Zenject.IFactory<ISocketIOConnection>>().To<ConnectionFactory>().AsSingle();
        Container.Bind<INetwork>().To<Network>().AsSingle();
        Container.Bind<INetworkConnection>().To<Network>().AsSingle();
        Container.Inject(this);
    }

    [Test]
    public void Run_Test()
    {
        //Login
    }
}
