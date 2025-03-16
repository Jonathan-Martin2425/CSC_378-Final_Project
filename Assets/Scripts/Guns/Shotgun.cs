using UnityEngine;
using System;
using System.Collections.Generic;

public class Shotgun : Gun
{
    [Header ("Shotgun settings")]
    public int numPellets = 8;
    public float spread = 45f;
    public int bulletsPerReload = 1;
    [SerializeField] List<Transform> firePoints = new List<Transform>();
    public override void Fire(Vector2 direction){
        if (onCooldown == false && currentAmmo != 0)
        {
            //randomizes rotation for all potential firepoints
            foreach(Transform p in firePoints){
                float randRoation = 90f + UnityEngine.Random.Range(-spread/2, spread/2);
                p.localRotation = Quaternion.Euler(Vector3.forward * randRoation);
            }
            for(int i = 0; i < numPellets; i++){
                //randomizes firePoint point
                int randFirepoint = UnityEngine.Random.Range(0, firePoints.Count - 1);
                GameObject bullet = Instantiate(bulletPrefab, firePoints[randFirepoint].position, firePoints[randFirepoint].rotation);
                
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = firePoints[randFirepoint].up * bulletSpeed;
                }
                
                //sets bullet data after instantiation
                DestroyBullet bulletScript = bullet.GetComponent<DestroyBullet>();
                bulletScript.setLevel(level);
                bulletScript.SetDamage(bulletDamage);

            }

            currentAmmo -= 1;

            if (currentAmmo == 0)
            {
                Reload(bulletsPerReload);
            }
            StartCoroutine(FireCooldown(1 / fireRate));

        }
    }

}
