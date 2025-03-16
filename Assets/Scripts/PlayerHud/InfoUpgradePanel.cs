using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Reflection;

public class InfoPanel : MonoBehaviour
{
    public Gun[] weapons;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI matsText;
    public Button upgradeButton;
    [SerializeField] public PlayerWeaponController playerWeaponController;
    [SerializeField] private PlayerMats mats;
    Gun currentWeapon;
    private int[] levels = new int[4];
    
    // {weaponId = {weaponLevel = {fieldName = upgradeValue}}}
    private Dictionary<int, Dictionary<int,string>> upgradePathsText =
        new()
        {
            {0, new Dictionary<int, string>
                {
                    {0, "Increased mag size."},
                    {1, "Get second pistol, Akimbo style."},
                    {2, "Pistol now have burst fire."},

                }
            },
            {1, new Dictionary<int, string>
                {
                    {0, "Sniper 1."},
                    {1, "Sniper 2."},
                    {2, "Sniper 3."},
                }
            },
            {2, new Dictionary<int, string>
                {
                    {0, "Shotgun does knockback."},
                    {1, "More pellets, wider spread."},
                    {2, "Fireeeeee."},
                }
            },
        };

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
                // old dictionary idea
                // maybe include with some methods
                /*foreach (var upgrade in upgradePaths[currentWeapon.id][currentWeapon.level])
                {
                    FieldInfo field = currentWeapon.GetType().GetField(upgrade.Key);
                    float currentValue = (float)field.GetValue(currentWeapon);
                    field.SetValue(currentWeapon, currentValue + upgrade.Value);

                    Debug.Log("Upgraded pistol " + upgrade.Key + " from "
                        + currentValue + " to " + field.GetValue(currentWeapon));
                }*/

                levels[currentWeapon.id] += 1;
                currentWeapon.level += 1;

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
