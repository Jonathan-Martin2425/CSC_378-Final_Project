using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InfoPanel : MonoBehaviour
{
    public Gun[] weapons;
    public TextMeshProUGUI infoText;
    public Button upgradeButton;
    private Gun currentWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWeapon = weapons[0];
        UpdateInfo();
    }

    void UpdateInfo()
    {
        infoText.text =  " Damage: " + currentWeapon.bulletDamage + "\n" + 
        " Rate of Fire: " + currentWeapon.fireRate + "\n" + 
        " Reload Speed: " + currentWeapon.reloadTimeSeconds + "\n" + 
        " Magazine Capacity: " + currentWeapon.magSize;
        // change images here
    }

    public void UpgradeWeapon()
    {
        if (currentWeapon.id == 0)
        {
            Debug.Log("UPRADE PISTOL");
        }
        else if (currentWeapon.id == 1)
        {
            Debug.Log("UPRADE SNIPER");
        }
        else if (currentWeapon.id == 2)
        {
            Debug.Log("UPRADE SHOTGUN");
        }
        else if (currentWeapon.id == 3)
        {
            Debug.Log("UPRADE GRENADE");
        }

        UpdateInfo();
    }

    public void SelectWeapon(int id)
    {
        currentWeapon = weapons[id];
        UpdateInfo();
    }
}
