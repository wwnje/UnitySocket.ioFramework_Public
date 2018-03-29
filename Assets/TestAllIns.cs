using System.Collections.Generic;
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

    public DlgLogin DlgLogin;
    public DlgLoginChangeServer DlgLoginChangeServer;
    public DlgLoginLoading DlgLoginLoading;
    public DlgLoginSetting DlgLoginSetting;
    public DlgLoginAccount DlgLoginAccount;
    public DlgLoginTip DlgLoginTip;


    public float Version = 1.2f;
    public List<string> loadingTipLst = new List<string> {"tip1", "tip2", "tip3" };

    void Awake()
    {
        DlgLoginChangeServer.gameObject.SetActive(false);
        DlgLoginLoading.gameObject.SetActive(false);
        DlgLoginSetting.gameObject.SetActive(false);
        DlgLoginAccount.gameObject.SetActive(false);
        DlgLoginTip.gameObject.SetActive(false);
    }
}
