using System.Collections;

public interface ILoadServerInfo
{
    IEnumerator LoadServerInfo();

    ServerInfo GetServerInfo();
}
