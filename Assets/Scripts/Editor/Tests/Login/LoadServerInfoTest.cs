using NUnit.Framework;
using Zenject;
using UniRx;
using Game.Login;
using UnityEngine;
using Game.Login.Internal;

[TestFixture]
public class LoadServerInfoTest : ZenjectUnitTestFixture
{
    ServerInfoSetting setting = new ServerInfoSetting();

    [Inject]
    IServerInfoLoader loader_online = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<IServerInfoLoader>().To<ServerInfoOnLineLoader>().AsSingle();
        Container.BindInstance(setting);
        Container.Inject(this);
    }

    const int VERSION_NOW = 1;

    [Test]
    public void TestSample()
    {
        Debug.Log("TestSample");
    }

    [Test]
    public void Run_Test_Online()
    {
        loader_online.Load()
        .Subscribe(info =>
        {
            Debug.Log(info.announcement);

            Assert.IsNotNull(info);
            Assert.AreEqual(info.vision, VERSION_NOW);
        });
    }
}
