using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class DlgLoginAccount : MonoBehaviour
{
    public Button Button_Close;

    // Use this for initialization
    void Awake()
    {
        Button_Close.OnClickAsObservable().Subscribe(_ => OnClose());
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
    }

    void OnClose()
    {
        gameObject.SetActive(false);
    }
}
