using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;

public class InfoPanel : MonoBehaviour
{
    public Gun[] weapons;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI matsText;
    public Button upgradeButton;
    [SerializeField] public PlayerWeaponController playerWeaponController;
    [SerializeField] private PlayerMats mats;
    Gun currentWeapon;
    //
    private int[] levels = new int[4];

    void OnEnable()
    {
        if (!playerWeaponController)
            playerWeaponController = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>();
        if (!mats)
            mats = GameObject.FindWithTag("Player").GetComponent<PlayerMats>();

        currentWeapon = playerWeaponController.currentWeapon;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        infoText.text =  " Damage: " + currentWeapon.bulletDamage + "\n" + 
        " Rate of Fire: " + currentWeapon.fireRate + "\n" + 
        " Reload Speed: " + currentWeapon.reloadTimeSeconds + "\n" + 
        " Magazine Capacity: " + currentWeapon.magSize;
        matsText.text = $"Materials Needed: {mats.mats}/{currentWeapon.costsPerLevel[currentWeapon.level]}";
        // change images here
    }

    public void UpgradeWeapon()
    {
        if (currentWeapon.id == 0)
        {
            if (currentWeapon.costsPerLevel[currentWeapon.level] <= mats.mats){
                Debug.Log("UPGRADE PISTOL");
                mats.UpgradeCost(currentWeapon.costsPerLevel[currentWeapon.level]);
                UpgradePistol(levels[currentWeapon.id]);
            }else{
                Debug.Log("not enough mats");
            }
        }
        else if (currentWeapon.id == 1)
        {
            if (currentWeapon.costsPerLevel[currentWeapon.level] <= mats.mats){
                Debug.Log("UPGRADE SNIPER");
                mats.UpgradeCost(currentWeapon.costsPerLevel[currentWeapon.level]);
                UpgradeSniper(levels[currentWeapon.id]);
            }else{
                Debug.Log("not enough mats");
            }
        }
        else if (currentWeapon.id == 2)
        {
            if (currentWeapon.costsPerLevel[currentWeapon.level] <= mats.mats){
                Debug.Log("UPGRADE SHOTGUN");
                mats.UpgradeCost(currentWeapon.costsPerLevel[currentWeapon.level]);
                UpgradeShotgun(levels[currentWeapon.id]);
            }else{
                Debug.Log("not enough mats");
            }
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
                currentWeapon.level += 1;
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
                currentWeapon.level += 1;
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

    void UpgradeShotgun(int level)
    {
        switch(level)
        {
            case 0:
                levels[currentWeapon.id] += 1;
                currentWeapon.level += 1;
                break;
            default:
                Debug.Log("Shotgun fully upgraded");
                break;
        }
    }

    public void SelectWeapon(int id)
    {
        currentWeapon = weapons[id];
        UpdateInfo();
    }
}
