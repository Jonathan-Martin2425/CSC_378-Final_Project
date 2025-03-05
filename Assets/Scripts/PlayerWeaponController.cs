using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerWeaponController : MonoBehaviour
{
    // Gun prefab will be parented to weaponMount on instantiation
    public Transform weaponMount;
    public Gun[] weapons;
    public Gun currentWeapon;
    public float swapTimer = 1f;
    [Header("Ammo Settings")]
    public int maxPistolAmmo = 999;
    public int maxRifleAmmo = 999;
    public int reservedPistolAmmo = 15;
    public int reservedSniperAmmo = 5;

    [Header("Gun UI")]
    public GameObject[] weaponSlots;
    public Color selectedColor = new Color(231, 210, 34);
    public HudStats hudStats;
    private Color defaultButtonColor;
    private bool canSwapWeapon = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultButtonColor = weaponSlots[0].GetComponent<Image>().color;
        weaponSlots[0].GetComponent<Image>().color = selectedColor;
        
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
        currentWeapon = weapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPoint2D = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Mathf.Atan2(worldPoint2D.y, worldPoint2D.x) * Mathf.Rad2Deg;

        // Add 180 to angle because rotation is opposite otherwise... for some reason
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Fire(worldPoint2D);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(1);
        }
        
        hudStats.UpdateAmmo();
    }

    public void SwitchGun(int gunID)
    {
        if (gunID != currentWeapon.id && canSwapWeapon == true)
        {
            weapons[currentWeapon.id].gameObject.SetActive(false);
            weapons[gunID].gameObject.SetActive(true);
            HighlightButton(gunID);
            currentWeapon = weapons[gunID];
            StartCoroutine(SwapCooldown(swapTimer));
        }
    }

    IEnumerator SwapCooldown(float seconds)
    {
        canSwapWeapon = false;
        yield return new WaitForSeconds(seconds);
        canSwapWeapon = true;
    }

    void HighlightButton(int id)
    {
        weaponSlots[id].GetComponent<Image>().color = selectedColor;
        weaponSlots[currentWeapon.id].GetComponent<Image>().color = defaultButtonColor;
    }

    public void AddAmmo(int count)
    {
        if (currentWeapon.id == 0)
        {
            reservedPistolAmmo += count;
            if (reservedPistolAmmo > maxPistolAmmo)
                reservedPistolAmmo = maxPistolAmmo;
        }
        else if (currentWeapon.id == 1)
        {
            reservedSniperAmmo += count;
            if (reservedSniperAmmo > maxRifleAmmo)
                reservedSniperAmmo = maxRifleAmmo;
        }
    }

    public int GetCurrentReservedAmmo()
    {
        if (currentWeapon.id == 0)
            return reservedPistolAmmo;
        else if (currentWeapon.id == 1)
            return reservedSniperAmmo;
        else
            Debug.LogError("No weapon selected");
        return 0;
    }

    public void ReloadWeapon()
    {
        if (currentWeapon.id == 0)
            ReloadPistol();
        else if (currentWeapon.id == 1)
            ReloadSniper();
    }

    public void ReloadPistol()
    {
        if (reservedPistolAmmo > 0)
        {
            int ammoNeeded = currentWeapon.magSize - currentWeapon.currentAmmo;
            if (reservedPistolAmmo >= ammoNeeded)
            {
                reservedPistolAmmo -= ammoNeeded;
                currentWeapon.currentAmmo = currentWeapon.magSize;
            }
            else
            {
                currentWeapon.currentAmmo += reservedPistolAmmo;
                reservedPistolAmmo = 0;
            }
        }
    }

    void ReloadSniper()
    {
        if (reservedSniperAmmo > 0)
        {
            int ammoNeeded = currentWeapon.magSize - currentWeapon.currentAmmo;
            if (reservedSniperAmmo >= ammoNeeded)
            {
                reservedSniperAmmo -= ammoNeeded;
                currentWeapon.currentAmmo = currentWeapon.magSize;
            }
            else
            {
                currentWeapon.currentAmmo += reservedSniperAmmo;
                reservedSniperAmmo = 0;
            }
        }
    }

}
