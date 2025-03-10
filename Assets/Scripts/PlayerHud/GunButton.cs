using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GunButton : MonoBehaviour
{
    public GameObject player;
    public Gun gun;
    public TextMeshProUGUI gunInfo;

    public void GunSelected()
    {
        player.GetComponent<PlayerWeaponController>().SwitchGun(gun.id);
    }

    public void GunDeselected()
    {

    }
    
    public void GunHover()
    {
        gunInfo.text = "<align=center>" + gun.gunName + "\n" + 
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
