using NUnit.Framework;
using System.IO;
using UnityEngine;
using Zenject;

[TestFixture]
public class GetServerLstTest : ZenjectUnitTestFixture
{
    public class MockLoadServerInfoFactory : Zenject.IFactory<ILoadServerInfo>
    {
        public ILoadServerInfo Create()
        {
            return new MockLoadServerInfoLocal();
        }
    }

    public class MockLoadServerInfoLocal : ILoadServerInfo
    {
        public ServerInfo GetServerInfo()
        {
            string filePath = Application.streamingAssetsPath + "/server_list.json";
            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                return JsonUtility.FromJson<ServerInfo>(dataAsJson);
            }

            return null;
        }
    }

    public class MockLoadServerInfoOnLine : ILoadServerInfo
    {
        public ServerInfo GetServerInfo()
        {
            string filePath = Application.streamingAssetsPath + "/server_list.json";
            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                return JsonUtility.FromJson<ServerInfo>(dataAsJson);
            }

            return null;
        }
    }

    [Inject]
    Zenject.IFactory<ILoadServerInfo> factory = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Zenject.IFactory<ILoadServerInfo>>().To<MockLoadServerInfoFactory>().AsSingle();
        Container.Inject(this);
    }

    const int VERSION_NOW = 1;

    [Test]
    public void Run_Test()
    {
        //Login 前获得服务列表
        Login login = new Login(factory);
        ServerInfo info = login.LoadServerInfo();
        Assert.IsNotNull(info);
        Assert.AreEqual(info.vision, VERSION_NOW);
    }
}
