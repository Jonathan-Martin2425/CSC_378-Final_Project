using UnityEngine;
using System.Collections.Generic;

public class SniperRifle : Gun
{
    public int pierce = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire(Vector2 direction)
    {
        if (onCooldown == false && currentAmmo > 0)
        {
            FireBullet(direction);
            
            if (currentAmmo <= 0)
            {
                Reload();
            }

            StartCoroutine(FireCooldown(1 / fireRate));
        }
    }

    private void FireBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<DestroyBullet>().SetDamage(bulletDamage);
        bullet.GetComponent<DestroyBullet>().numPierce = pierce;

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        currentAmmo -= 1;

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
    }
}
