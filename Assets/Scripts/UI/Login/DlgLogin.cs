using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Zenject;

public class DlgLogin : MonoBehaviour
{
    public Button ButtonStartGame;
    public Button ButtonSetting;

    LoginStartGameSignal StartGameSignal = new LoginStartGameSignal();
    LoginOpenSettingSignal OpenSettingSignal = new LoginOpenSettingSignal();

    // Use this for initialization
    void Start()
    {
        StartGameSignal.Subscribe(_ =>
        {
            TestAllIns.Ins.DlgLoginLoading.SetActive(true);
            this.gameObject.SetActive(false);
        }
        );

        OpenSettingSignal.Subscribe(_ =>
        {
            TestAllIns.Ins.DlgLoginSetting.SetActive(true);
            this.gameObject.SetActive(false);
        }
        );

        ButtonStartGame.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StartGameSignal.Fire();
            });

        ButtonSetting.OnClickAsObservable()
            .Subscribe(_ =>
            {
                OpenSettingSignal.Fire();
            });
    }
}
