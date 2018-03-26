using NUnit.Framework;
using System.IO;
using UnityEngine;
using Zenject;

[TestFixture]
public class GetServerLstTest : ZenjectUnitTestFixture
{
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

    [Inject]
    ILoadServerInfo loadServerInfo = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ILoadServerInfo>().To<MockLoadServerInfoLocal>().AsSingle();
        Container.Inject(this);
    }

    const int VERSION_NOW = 1;

    [Test]
    public void Run_Test()
    {
        //Login 前获得服务列表
        ServerInfo info = loadServerInfo.GetServerInfo();
        Assert.IsNotNull(info);
        Assert.AreEqual(info.vision, VERSION_NOW);
    }
}
