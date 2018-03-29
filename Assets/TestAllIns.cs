using UnityEngine;

public class TestAllIns : MonoBehaviour
{
    private static TestAllIns _ins;
    public static TestAllIns Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = FindObjectOfType<TestAllIns>();
            }
            return _ins;
        }
    }

    public GameObject DlgLogin;
    public GameObject DlgLoginChangeServer;
    public GameObject DlgLoginLoading;
    public GameObject DlgLoginSetting;
}
