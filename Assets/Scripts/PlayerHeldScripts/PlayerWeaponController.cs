using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class PlayerWeaponController : MonoBehaviour
{
    // Gun prefab will be parented to weaponMount on instantiation
    public Transform weaponMount;
    public Gun[] weapons;
    public Gun currentWeapon;
    public float swapTimer = 1f;
    [Header("Ammo Settings")]
    public int maxAmmo = 999;
    public int reservedPistolAmmo = 15;
    public int reservedSniperAmmo = 5;
    public int reservedShotgunAmmo = 8;

    public List<int> reservedAmmo = new List<int>();

    [Header("Gun UI")]
    public GameObject[] weaponSlots;
    public Color selectedColor = new Color(231, 210, 34);
    public GameObject swapOverlay;
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

        reservedAmmo.Add(reservedPistolAmmo);
        reservedAmmo.Add(reservedSniperAmmo);
        reservedAmmo.Add(reservedShotgunAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponInfoController.isPaused)
        {
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 worldPoint2D = mousePosition-transform.position;
        worldPoint2D.Normalize();
        float angle = Mathf.Atan2(worldPoint2D.y, worldPoint2D.x) * Mathf.Rad2Deg;

        // Add 180 to angle because rotation is opposite otherwise... for some reason
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(!currentWeapon.isReloading){
                currentWeapon.Fire(worldPoint2D);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGun(2);
        }
        
        GameObject.FindWithTag("UI").GetComponent<HudStats>().UpdateAmmo(currentWeapon.currentAmmo, GetCurrentReservedAmmo());
    }

    public void SwitchGun(int gunID)
    {
        if (gunID != currentWeapon.id && canSwapWeapon == true)
        {
            weapons[currentWeapon.id].gameObject.SetActive(false);
            weapons[gunID].gameObject.SetActive(true);
            weapons[gunID].ammoIcon.rotation = Quaternion.identity;
            HighlightButton(gunID);
            currentWeapon = weapons[gunID];
            StartCoroutine(SwapCooldown(swapTimer));
        }
    }

    IEnumerator SwapCooldown(float seconds)
    {
        canSwapWeapon = false;
        UpdateSwapOverlay(!canSwapWeapon);
        yield return new WaitForSeconds(seconds);
        canSwapWeapon = true;
        UpdateSwapOverlay(!canSwapWeapon);
    }

    private void UpdateSwapOverlay(bool onCooldown)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (currentWeapon.id == i)
            {
                continue;
            }

            if (onCooldown)
            {   
                GameObject tempOverlay = Instantiate(swapOverlay, weaponSlots[i].transform);
                tempOverlay.name = "SwapOverlay";
                tempOverlay.GetComponent<RectTransform>().localScale = weaponSlots[i].GetComponent<RectTransform>().sizeDelta;
            }
            else
            {
                Transform toRemove = weaponSlots[i].transform.Find("SwapOverlay");
                Destroy(toRemove.gameObject);
            }
        }
    }

    private void RemoveSwapOverlay()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (currentWeapon.id == i)
            {
                continue;
            }


        }
    }

    void HighlightButton(int id)
    {
        weaponSlots[id].GetComponent<Image>().color = selectedColor;
        weaponSlots[currentWeapon.id].GetComponent<Image>().color = defaultButtonColor;
    }

    public void AddAmmo(int count, int id)
    {
        //used index to determine maxAmmo, a list could also be used for maxAmmo quantites
        // right now used pistol's max ammo
         if(id < reservedAmmo.Count)
         {
            reservedAmmo[id] += count;
            if (reservedAmmo[id] > maxAmmo)
                reservedAmmo[id] = maxAmmo;
        }
        else
        {
            Debug.LogError("No weapon selected");
        }
    }

    public int GetCurrentReservedAmmo()
    {
        //now uses reservedAmmo list, instead of both varriables
        if(currentWeapon.id < reservedAmmo.Count)
         {
            return reservedAmmo[currentWeapon.id];
        }
        else
        {
            Debug.LogError("No weapon selected");
        }
        return 0;
    }

    public void ReloadWeapon()
    {
        //now uses reservedAmmo list, instead of both varriables
        if (reservedAmmo[currentWeapon.id] > 0)
        {
            int ammoNeeded = currentWeapon.magSize - currentWeapon.currentAmmo;
            if (reservedAmmo[currentWeapon.id] >= ammoNeeded)
            {
                reservedAmmo[currentWeapon.id] -= ammoNeeded;
                currentWeapon.currentAmmo = currentWeapon.magSize;
            }
            else
            {
                currentWeapon.currentAmmo += reservedAmmo[currentWeapon.id];
                reservedAmmo[currentWeapon.id] = 0;
            }
        }
    }

    public void ReloadWeapon(int ammoToAdd)
    {
        //now uses reservedAmmo list, instead of both varriables
        if (reservedAmmo[currentWeapon.id] > 0)
        {
            if (reservedAmmo[currentWeapon.id] >= ammoToAdd)
            {
                currentWeapon.currentAmmo += ammoToAdd;
                if(currentWeapon.currentAmmo > currentWeapon.magSize){
                    int ammoAdded = ammoToAdd - (currentWeapon.currentAmmo - currentWeapon.magSize);
                    currentWeapon.currentAmmo = currentWeapon.magSize;
                    reservedAmmo[currentWeapon.id] -= ammoAdded;
                }else{
                    reservedAmmo[currentWeapon.id] -= ammoToAdd;
                }
            }
            else
            {
                currentWeapon.currentAmmo += reservedAmmo[currentWeapon.id];
                reservedAmmo[currentWeapon.id] = 0;
            }
        }
    }

    void OnReload(){
        if (!currentWeapon.isReloading && currentWeapon.currentAmmo != currentWeapon.magSize && reservedAmmo[currentWeapon.id] > 0)
        {
            if(currentWeapon.GetType().Equals(typeof(Shotgun))){
                Shotgun gun = (Shotgun)currentWeapon;
                currentWeapon.Reload(gun.bulletsPerReload);
            }else{
                currentWeapon.Reload();
            }
        }
    }

}
