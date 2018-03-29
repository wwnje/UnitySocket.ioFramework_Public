using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DlgLoginSetting : MonoBehaviour
{
    public Button Button_Close;
    public Button Button_ChangeServer;
    public Button Button_Account;

    // Use this for initialization
    void Awake()
    {
        Button_Close.OnClickAsObservable().Subscribe(_ => OnClose());
        Button_ChangeServer.OnClickAsObservable().Subscribe(_ => OpenChangeServer());
        Button_Account.OnClickAsObservable().Subscribe(_ => OpenAccount());
    }

    void OpenChangeServer()
    {
        TestAllIns.Ins.DlgLoginChangeServer.OnOpen();
    }

    void OpenAccount()
    {
        TestAllIns.Ins.DlgLoginAccount.OnOpen();
    }

    void OnClose()
    {
        gameObject.SetActive(false);
    }
}
