// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public PlayerWeaponController weaponController;
    

    [Header("Gun Settings")]
    public int id;
    public string gunName;
    // Fire rate in bullets per second
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float bulletDamage = 1f;
    public float reloadTimeSeconds = 1f;
    public int level = 0;
    
    [Header("Ammo Settings")]
    public int magSize = 15;
    public int currentAmmo = 0;
    
    [Header ("Upgrade Settings")]
    public List<int> costsPerLevel = new List<int>();


    protected bool onCooldown = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (!weaponController)
            weaponController = GetComponent<PlayerWeaponController>();
        onCooldown = false;
        if (currentAmmo == 0)
        {
            Reload();
        }
    }

    public virtual void Fire(Vector2 direction)
    {
        if (onCooldown == false && currentAmmo != 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            bullet.GetComponent<DestroyBullet>().SetDamage(bulletDamage);

            if (rb != null)
            {
                currentAmmo -= 1;
                rb.linearVelocity = direction * bulletSpeed;
            }

            if (currentAmmo == 0)
            {
                Reload();
            }
            StartCoroutine(FireCooldown(1 / fireRate));
        }else{
            //Debug.Log("on fire cooldown");
        }
    }

    protected IEnumerator FireCooldown(float seconds)
    {
        onCooldown = true;
        yield return new WaitForSeconds(seconds);
        onCooldown = false;
    }

    public void Reload()
    {
        StartCoroutine(ReloadCooldown(reloadTimeSeconds));
    }

    public void Reload(int ammoToAdd)
    {
        StartCoroutine(ReloadCooldown(reloadTimeSeconds, ammoToAdd));
    }

    IEnumerator ReloadCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        weaponController.ReloadWeapon();
    }

    IEnumerator ReloadCooldown(float seconds, int ammoToAdd)
    {
        yield return new WaitForSeconds(seconds);
        weaponController.ReloadWeapon(ammoToAdd);
    }

}
