using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class DlgLoginChange_ServerChooseItem : MonoBehaviour
{
    public Button Button_Click;

    int itemIndex;
    Subject<int> subject = new Subject<int>();

    void Awake()
    {
        Button_Click.onClick.AddListener(OnClick);
    }


    public IObservable<int> Bind(int index)
    {
        itemIndex = index;
        return subject;
    }


    // Update is called once per frame
    void OnClick()
    {
        subject.OnNext(itemIndex);
    }
}
