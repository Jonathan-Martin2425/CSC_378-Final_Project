// using System.Numerics;
using System.Collections;
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
    

    [Header("Ammo Settings")]
    public int magSize = 15;
    public int currentAmmo = 0;


    bool onCooldown = false;
    
    
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

    public void Fire(Vector2 direction)
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
                onCooldown = true;
            }

            if (currentAmmo == 0)
            {
                Reload();
            }
            StartCoroutine(FireCooldown(1 / fireRate));
        }
    }

    IEnumerator FireCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        onCooldown = false;
    }

    public void Reload()
    {
        StartCoroutine(ReloadCooldown(reloadTimeSeconds));
    }

    IEnumerator ReloadCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        weaponController.ReloadWeapon();
    }
}
