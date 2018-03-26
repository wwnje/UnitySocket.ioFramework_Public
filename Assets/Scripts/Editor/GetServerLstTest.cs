using NUnit.Framework;
using Zenject;
using UnityEngine;

[TestFixture]
public class GetServerLstTest : ZenjectUnitTestFixture
{
    [Inject]
    Zenject.IFactory<ILoadServerInfo> factory = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Zenject.IFactory<ILoadServerInfo>>().To<LoadServerInfoFactory>().AsSingle();
        Container.Inject(this);
    }

    const int VERSION_NOW = 1;

    [Test]
    public void Run_Test()
    {
        //Login 前获得服务列表
        LoginIns.Ins.LoadServerInfo();
        ServerInfo info = LoginIns.Ins.GetServerInfo();

        Assert.IsNull(info);
        //Assert.AreEqual(info.vision, VERSION_NOW);
    }
}
