using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DlgLoginChangeServer : MonoBehaviour
{
    public ScrollRect PnlChooseList;
    public ScrollRect PnlDetailList;

    public GameObject ChooseItem;
    public GameObject DetailItem;

    Transform chooseItemParent;
    Transform detalItemParent;

    void Start()
    {
        chooseItemParent = PnlChooseList.content;
        detalItemParent = PnlDetailList.content;

        OnOpen();
    }

    public void OnOpen()
    {
        Refresh();
    }

    void Refresh()
    {
        // refresh
        foreach (Transform child in chooseItemParent)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject item = null;
            if (i > chooseItemParent.childCount - 1)
            {
                item = Instantiate(ChooseItem);
            }
            else
            {
                item = chooseItemParent.GetChild(i).gameObject;
            }

            item.transform.parent = chooseItemParent;
            item.GetComponentInChildren<Text>().text = i + "";

            // addOnclick
            item.GetComponent<DlgLoginChange_ServerChooseItem>().Bind(i)
                .Subscribe(_ => OnClick(_))
                .AddTo(this);

            item.SetActive(true);
        }
    }

    void OnClick(int i)
    {
        Debug.LogError(i);
    }
}
