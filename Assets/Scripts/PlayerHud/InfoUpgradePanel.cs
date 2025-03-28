using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

public class InfoPanel : MonoBehaviour
{
    [Header("Weapon Objects")]
    public Gun[] weapons;
    Gun currentWeapon;
    public List<Sprite> weaponIcons;
    [Header("UI Elements")]
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI upgradedText;
    public TextMeshProUGUI matsText;
    public Button upgradeButton;
    public Image weaponDisplay;
    [Header("Player Elements")]
    [SerializeField] public PlayerWeaponController playerWeaponController;
    [SerializeField] private PlayerMats mats;
    private int[] levels = new int[4];
    private Dictionary<int, Dictionary<int,string>> upgradePathsText =
        new()
        {
            {0, new Dictionary<int, string>
                {
                    {0, "Increased mag size."},
                    {1, "Increased mag size.\n Get second pistol, Akimbo style."},
                    {2, "Pistol now have burst fire."},
                    {3, "Pistol fully upgraded."}

                }
            },
            {1, new Dictionary<int, string>
                {
                    {0, "Increased fire rate."},
                    {1, "Bullets pierce up to 3 enemies."},
                    {2, "Railgun with hitscan and infinite pierce."},
                    {3, "Sniper fully upgraded."}
                }
            },
            {2, new Dictionary<int, string>
                {
                    {0, "Shotgun does knockback."},
                    {1, "More pellets, wider spread."},
                    {2, "Fireeeeee."},
                    {3, "Shotgun fully upgraded."}
                }
            },
        };

    void Start()
    {
        currentWeapon = weapons[0];
        UpdateInfo();
    }

    void OnEnable()
    {
        StartCoroutine(DelayUpdateInfo());
    }

    public void UpdateInfo()
    {
        infoText.text =  " Damage: " + currentWeapon.bulletDamage + "\n" + 
        " Rate of Fire: " + currentWeapon.fireRate + "\n" + 
        " Reload Speed: " + currentWeapon.reloadTimeSeconds + "\n" + 
        " Magazine Capacity: " + currentWeapon.magSize;
        matsText.text = $"Materials Needed: {mats.mats}/{currentWeapon.costsPerLevel[currentWeapon.level]}";

        DisplayWeapon();
        DisplayUpgrades();
    }

    private IEnumerator DelayUpdateInfo()
    {
        yield return null;
        UpdateInfo();
    }

    public void DisplayWeapon()
    {
        weaponDisplay.sprite = weaponIcons[currentWeapon.id];
    }

    public void DisplayUpgrades()
    {
        upgradedText.text = $" {upgradePathsText[currentWeapon.id][currentWeapon.level]}";
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
        Pistol pistol = (Pistol)currentWeapon;
        switch(level)
        {
            case 0:
                pistol.magSize *= 2;
                levels[currentWeapon.id] += 1;
                pistol.level += 1;
                break;
            case 1:
                pistol.isDual = true;
                pistol.magSize *= 2;
                pistol.pistolSprites[0].SetActive(false);
                pistol.pistolSprites[1].SetActive(true);
                pistol.pistolSprites[2].SetActive(true);
                levels[currentWeapon.id] += 1;
                pistol.level += 1;
                break;
            case 2:
                pistol.hasBurstUpgrade = true;
                levels[currentWeapon.id] += 1;
                pistol.level += 1;
                break;
            default:
                Debug.Log("Pistol fully upgraded");
                break;
        }
    }

    void UpgradeSniper(int level)
    {
        SniperRifle sniper = (SniperRifle)currentWeapon;
        switch(level)
        {
            case 0:
                sniper.fireRate = 1.5f;
                levels[currentWeapon.id] += 1;
                sniper.level += 1;

                break;
            case 1:
                sniper.pierce = 3;
                levels[currentWeapon.id] += 1;
                sniper.level += 1;

                break;
            case 2:
                sniper.isRailgun = true;

                //set magsize to 3
                if(sniper.currentAmmo > 3){
                    int ammoDiff = sniper.currentAmmo - 3;
                    playerWeaponController.AddAmmo(ammoDiff, sniper.id);
                    sniper.currentAmmo = 3;
                }
                sniper.magSize = 3;

                sniper.reloadTimeSeconds = 3;
                sniper.fireRate = 1/2.5f;
                levels[currentWeapon.id] += 1;
                sniper.level += 1;

                break;
            default:
                Debug.Log("Sniper fully upgraded");

                break;
        }
    }

    void UpgradeShotgun(int level)
    {
        Shotgun shotgun = (Shotgun)currentWeapon;
        switch(level)
        {
            case 0:
                levels[currentWeapon.id] += 1;
                shotgun.level += 1;
                break;
            case 1:
                shotgun.spread = 45f;
                shotgun.numPellets = 8;
                levels[currentWeapon.id] += 1;
                shotgun.level += 1;
                break;
            case 2:
                levels[currentWeapon.id] += 1;
                shotgun.level += 1;
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

    public void ClickTowerRepair()
    {
        GameObject.FindWithTag("Tower").GetComponent<TowerHealth>().RepairTower();
        UpdateInfo();
    }
}