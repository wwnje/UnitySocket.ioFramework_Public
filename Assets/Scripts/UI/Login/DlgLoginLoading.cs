using UnityEngine;
using UnityEngine.UI;

public class DlgLoginLoading : MonoBehaviour
{
    public Slider LoadingSlider;
    public Text TextVersion;
    public Text TextTip;
    public RectTransform SliderLoadingIcon;
    public RectTransform FillArea;

    float tipDeltaTime = 3.0f;
    float tipTime = 0f;
    int tipIdx = 0;
    int tipNum = 3;
    bool loaded = false;

    TestAllIns _manager;
    public TestAllIns Manager
    {
        get
        {
            if (_manager == null)
            {
                _manager = TestAllIns.Ins;
            }
            return _manager;
        }
    }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        gameObject.SetActive(true);
        LoadingSlider.value = 0;
        TextVersion.text = "版本号:" + Manager.Version;
        TextTip.text = Manager.loadingTipLst[0];
        loaded = false;
        tipTime = 0f;
    }

    public void OnOpenStartLoading()
    {
        Init();

        //跳转到加载界面，加载界优先处理版本验证，由客户端向服务端发送版本号进行验证，版本不符则进行更新处理，版本验证通过，则加载数据，成功跳转游戏界面，具体如下
        CheckVersion();
        Invoke("CheckVersionBack", 2f);
    }

    void CheckVersion()
    {
        //check 版本验证
        Debug.Log("发送 版本验证");
    }

    void CheckVersionBack()
    {
        bool checkSuccess = false;
        if (checkSuccess)
        {
            Debug.Log("版本验证 正确");
        }
        else
        {
            Debug.LogError("版本验证 错误");
            TestAllIns.Ins.DlgLoginTip.OnOpen();
        }
    }

    void Update()
    {
        tipTime += Time.deltaTime;

        if (tipTime >= tipDeltaTime)
        {
            tipTime = 0;
            TextTip.text = Manager.loadingTipLst[(++tipIdx) % tipNum];
        }

        if (loaded) return;

        LoadingSlider.value = Mathf.Min(LoadingSlider.value + Time.unscaledDeltaTime * 1f, 1f);

        float x = FillArea.rect.x + FillArea.rect.width * LoadingSlider.value;
        float y = FillArea.rect.y + FillArea.rect.height / 2;

        SliderLoadingIcon.anchoredPosition = new Vector2(x, y);

        if (LoadingSlider.value > 0.99f)
        {
            loaded = true;
            Debug.Log("Enter GameScene");
        }
    }
}
