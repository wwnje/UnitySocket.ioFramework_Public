using UnityEngine;
using UnityEngine.UI;

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
            item.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
