using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunDisplayButton : MonoBehaviour
{
    private Color toggledColor = new Color(231f / 255, 210f / 255f, 34f / 255f);
    private Color offColor;

    void Start()
    {
        offColor = GetComponentInChildren<Image>().color;
    }

    void Update()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GetComponentInChildren<Image>().color = toggledColor;
        }
        else
        {
            GetComponentInChildren<Image>().color = offColor;
        }
    }
}
