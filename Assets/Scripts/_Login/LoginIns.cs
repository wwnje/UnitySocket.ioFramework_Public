using System.Collections;
using UnityEngine;

public class LoginIns : MonoBehaviour
{
    private static LoginIns _ins;
    public static LoginIns Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = FindObjectOfType<LoginIns>();

                if (_ins == null)
                {
                    _ins = new GameObject().AddComponent<LoginIns>();
                }
            }
            return _ins;
        }
    }

    ILoadServerInfo loadServerInfo = null;
    Zenject.IFactory<ILoadServerInfo> factory = new LoadServerInfoFactory();

    void Awake()
    {
        loadServerInfo = factory.Create();
    }

    public void LoadServerInfo()
    {
        StartCoroutine(loadServerInfo.LoadServerInfo());
    }

    public ServerInfo GetServerInfo()
    {
        if (loadServerInfo == null)
        {
            return null;
        }
        return loadServerInfo.GetServerInfo();
    }
}
