using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class DlgLogin : MonoBehaviour
{
    public Button ButtonStartGame;
    public Button ButtonSetting;

    LoginStartGameSignal StartGameSignal = new LoginStartGameSignal();
    LoginOpenSettingSignal OpenSettingSignal = new LoginOpenSettingSignal();

    // Use this for initialization
    void Awake()
    {
        Test();

        ButtonStartGame.OnClickAsObservable().Subscribe(_ => OnClickStartGame());
        ButtonSetting.OnClickAsObservable().Subscribe(_ => OnClickSetting());
    }

    void Test()
    {
        StartGameSignal.Subscribe(_ =>
        {
            TestAllIns.Ins.DlgLoginLoading.OnOpenStartLoading();
        });

        OpenSettingSignal.Subscribe(_ =>
        {
            TestAllIns.Ins.DlgLoginSetting.gameObject.SetActive(true);
        });
    }

    void OnClickStartGame()
    {
        //Check
        //无人物账号或已有账号登录后
        Debug.Log("人物登录");
        StartGameSignal.Fire();
    }

    void OnClickSetting()
    {
        OpenSettingSignal.Fire();
    }

    void OnClose()
    {
        // when enter mainScene
        gameObject.SetActive(false);
    }
}
