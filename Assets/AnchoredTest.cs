using UnityEngine;

public class AnchoredTest : MonoBehaviour {

    RectTransform m_RectTransform;
    float m_XAxis, m_YAxis;

    void Start()
    {
        //Fetch the RectTransform from the GameObject
        m_RectTransform = GetComponent<RectTransform>();
        //Initiate the x and y positions
        m_XAxis = 0.5f;
        m_YAxis = 0.5f;
    }

    void OnGUI()
    {
        //The Labels show what the Sliders represent
        GUI.Label(new Rect(0, 20, 150, 80), "Anchor Position X : ");
        GUI.Label(new Rect(300, 20, 150, 80), "Anchor Position Y : ");

        //Create a horizontal Slider that controls the x and y Positions of the anchors
        m_XAxis = GUI.HorizontalSlider(new Rect(150, 20, 100, 80), m_XAxis, -50.0f, 50.0f);
        m_YAxis = GUI.HorizontalSlider(new Rect(450, 20, 100, 80), m_YAxis, -50.0f, 50.0f);

        //Detect a change in the GUI Slider
        if (GUI.changed)
        {
            //Change the RectTransform's anchored positions depending on the Slider values
            m_RectTransform.anchoredPosition = new Vector2(m_XAxis, m_YAxis);
            //m_RectTransform.position = new Vector2(m_XAxis, m_YAxis);
        }
    }
}
