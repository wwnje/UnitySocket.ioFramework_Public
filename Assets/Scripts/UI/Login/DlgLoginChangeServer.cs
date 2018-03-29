using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DlgLoginChangeServer : MonoBehaviour
{
    public ScrollRect PnlChooseList;
    public ScrollRect PnlDetailList;

    public GameObject ChooseItem;
    public GameObject DetailItem;

    public Button Button_Close;

    Transform chooseItemParent;
    Transform detalItemParent;

    void Awake()
    {
        Button_Close.OnClickAsObservable().Subscribe(_ => OnClose());

        chooseItemParent = PnlChooseList.content;
        detalItemParent = PnlDetailList.content;
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
        RefreshChooseLst();
    }

    void RefreshChooseLst()
    {
        // refresh
        foreach (Transform child in chooseItemParent)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = i;
            GameObject item = null;

            if (index > chooseItemParent.childCount - 1)
            {
                item = Instantiate(ChooseItem);
            }
            else
            {
                item = chooseItemParent.GetChild(index).gameObject;
            }

            item.transform.parent = chooseItemParent;
            item.GetComponentInChildren<Text>().text = index + "";

            // addOnclick
            item.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ => OnClick(index));

            item.SetActive(true);
        }
    }

    void OnClick(int i)
    {
        // change detail
        ChangeDetail(i);
    }

    void ChangeDetail(int index)
    {
        // refresh
        foreach (Transform child in detalItemParent)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < index + 1; i++)
        {
            GameObject item = null;

            if (i > detalItemParent.childCount - 1)
            {
                item = Instantiate(DetailItem);
            }
            else
            {
                item = detalItemParent.GetChild(i).gameObject;
            }

            item.transform.parent = detalItemParent;
            item.GetComponentInChildren<Text>().text = i + "";
            item.SetActive(true);
        }
    }

    void OnClose()
    {
        gameObject.SetActive(false);
    }
}
