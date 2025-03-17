using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class Pistol : Gun
{
    [SerializeField] List<Transform> firePoints = new List<Transform>();
    public List<GameObject> pistolSprites;
    public bool isDual = false;
    public bool hasBurstUpgrade = false;
    private bool canBurst = true;
    public int burstCount = 3;
    public float burstDelay = 0.2f;
    public AudioSource dualSound;

    public override void Fire(Vector2 direction){
        if (onCooldown == false && currentAmmo > 0)
        {
            if (isDual)
            {
                if (hasBurstUpgrade && canBurst)
                {
                    StartCoroutine(Burst());
                }
                else
                {
                    FireBullet(direction);
                }
            }
            else
            {
                FireBullet(direction);
            }

            if (currentAmmo <= 0)
            {
                Reload();
            }
            StartCoroutine(FireCooldown(1 / fireRate));
        }
    }

    private IEnumerator Burst()
    {
        canBurst = true;
        for (int i = 0; i < burstCount; i++)
        {
            if (currentAmmo <= 0)
            {
                break;
            }

            // Recalculate the mouse position for each burst
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 worldPoint2D = mousePosition-transform.position;
            worldPoint2D.Normalize();
            float angle = Mathf.Atan2(worldPoint2D.y, worldPoint2D.x) * Mathf.Rad2Deg;
            // Add 180 to angle because rotation is opposite otherwise... for some reason
            transform.rotation = Quaternion.Euler(0, 0, angle + 180);

            FireBullet(worldPoint2D);
            yield return new WaitForSeconds(burstDelay);
        }
        canBurst = true;
        if (currentAmmo <= 0)
        {
            Reload();
        }
    }

    private void FireBullet(Vector2 direction)
    {
        if (isDual)
        {
            GameObject bulletL = Instantiate(bulletPrefab, firePoints[1].position, firePoints[1].rotation);
            GameObject bulletR = Instantiate(bulletPrefab, firePoints[2].position, firePoints[2].rotation);

            Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
            bulletL.GetComponent<DestroyBullet>().SetDamage(bulletDamage);
            bulletR.GetComponent<DestroyBullet>().SetDamage(bulletDamage);

            if (rbL != null)
            {
                rbL.linearVelocity = direction * bulletSpeed;
            }
            currentAmmo -= 1;

            if (rbR != null)
            {
                rbR.linearVelocity = direction * bulletSpeed;
            }
            currentAmmo -= 1;
            dualSound.Play();
        }
        else 
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<DestroyBullet>().SetDamage(bulletDamage);

            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }

            currentAmmo -= 1;
            fireSound.Play();
        }

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
    }
}
