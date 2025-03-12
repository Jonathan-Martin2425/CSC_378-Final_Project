using UnityEngine;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;

public class GunDisplayButton : MonoBehaviour
{
    public Gun gun;
    public TextMeshProUGUI infoText;
    private Color toggledColor = new Color(231f / 255, 210f / 255f, 34f / 255f);
    private Color offColor;

    void Start()
    {
        offColor = GetComponentInChildren<Image>().color;
        ShowInfo();
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

    public void ShowInfo()
    {
        infoText.text =  " Damage: " + gun.bulletDamage + "\n" + 
        " Rate of Fire: " + gun.fireRate + "\n" + 
        " Reload Speed: " + gun.reloadTimeSeconds + "\n" + 
        " Magazine Capacity: " + gun.magSize;
    }
}
