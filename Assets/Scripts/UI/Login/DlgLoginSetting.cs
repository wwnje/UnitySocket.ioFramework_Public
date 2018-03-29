using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DlgLoginSetting : MonoBehaviour
{
    public Button Button_Close;

    // Use this for initialization
    void Awake()
    {
        Button_Close.OnClickAsObservable().Subscribe(_ => OnClose());
    }
    void OnClose()
    {
        gameObject.SetActive(false);
    }
}
