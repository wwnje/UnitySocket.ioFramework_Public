using UnityEngine;

public class Login
{
    Zenject.IFactory<ILoadServerInfo> loadServerInfoFactory = null;
    ILoadServerInfo loadServerInfo = null;

    public Login(Zenject.IFactory<ILoadServerInfo> factory)
    {
        loadServerInfoFactory = factory;
    }
    public ServerInfo LoadServerInfo()
    {
        loadServerInfo = loadServerInfoFactory.Create();
        return loadServerInfo.GetServerInfo();
    }
}
