using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredFollow : MonoBehaviour
{

	// Update is called once per frame
	void Update () {
        RectTransform rectTransform = transform.parent.GetComponent<RectTransform>();
        //父物体 左上角
        GetComponent<RectTransform>().anchoredPosition = new Vector2(rectTransform.rect.x, rectTransform.rect.y + rectTransform.rect.height);
    }
}
