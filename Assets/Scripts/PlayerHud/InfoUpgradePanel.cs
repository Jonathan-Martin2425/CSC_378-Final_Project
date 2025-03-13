using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;

public class InfoPanel : MonoBehaviour
{
    public Gun[] weapons;
    public TextMeshProUGUI infoText;
    public Button upgradeButton;
    [SerializeField] private Gun currentWeapon;
    //
    private int[] levels = new int[4];

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
            Debug.Log("UPGRADE PISTOL");
            UpgradePistol(levels[currentWeapon.id]);
        }
        else if (currentWeapon.id == 1)
        {
            Debug.Log("UPGRADE SNIPER");
            UpgradeSniper(levels[currentWeapon.id]);
        }
        else if (currentWeapon.id == 2)
        {
            Debug.Log("UPGRADE SHOTGUN");
        }
        else if (currentWeapon.id == 3)
        {
            Debug.Log("UPGRADE GRENADE");
        }

        UpdateInfo();
    }

    void UpgradePistol(int level)
    {
        switch(level)
        {
            case 0:
                levels[currentWeapon.id] += 1;
                // currentWeapon.fireRate += 1;
                // currentWeapon.bulletSpeed += 1;
                currentWeapon.bulletDamage += 1;
                // currentWeapon.reloadTimeSeconds += 1;
                break;
            default:
                Debug.Log("Pistol fully upgraded");
                break;
        }
    }

    void UpgradeSniper(int level)
    {
        switch(level)
        {
            case 0:
                levels[currentWeapon.id] += 1;
                currentWeapon.fireRate += 1;
                // currentWeapon.bulletSpeed += 1;
                // currentWeapon.bulletDamage += 1;
                // currentWeapon.reloadTimeSeconds += 1;
                break;
            default:
                Debug.Log("Sniper fully upgraded");
                break;
        }
    }

    public void SelectWeapon(int id)
    {
        currentWeapon = weapons[id];
        UpdateInfo();
    }
}
