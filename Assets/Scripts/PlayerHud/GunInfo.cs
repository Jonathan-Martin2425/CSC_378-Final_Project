using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GunInfo : MonoBehaviour
{
    public Gun gun;
    public String gunName;
    public TextMeshProUGUI gunInfo;

    public void GunHover()
    {
        gunInfo.text = "<align=center>" + gunName + "\n" + 
        "<align=left>" + " Damage: " + gun.bulletDamage + "\n" + 
        " Rate of Fire: " + gun.fireRate + "\n" + 
        " Reload Speed: " + gun.reloadTimeSeconds + "\n" + 
        " Magazine Capacity: " + gun.magSize;
    }

    public void GunUnhover()
    {
        gunInfo.text = "";
    }
}
