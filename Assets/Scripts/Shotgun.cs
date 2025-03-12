using UnityEngine;
using System;
using System.Collections.Generic;

public class Shotgun : Gun
{
    public int numPellets = 8;
    public float spread = 45f;
    public int bulletsPerReload = 1;
    [SerializeField] List<Transform> firePoints = new List<Transform>();
    public override void Fire(Vector2 direction){
        if (onCooldown == false && currentAmmo != 0)
        {
            for(int i = 0; i < numPellets; i++){
                
                int randFirepoint = UnityEngine.Random.Range(0, firePoints.Count - 1);
                GameObject bullet = Instantiate(bulletPrefab, firePoints[randFirepoint].position, firePoints[randFirepoint].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                
                float randRoation = UnityEngine.Random.Range(-spread/2, spread/2);

                bullet.GetComponent<DestroyBullet>().SetDamage(bulletDamage);
                if (rb != null)
                {
                    Vector2 directionRotation = new Vector2((float)(Math.Acos(direction.x) * (180/Math.PI)), 
                                                            (float)(Math.Asin(direction.y) * (180/Math.PI)));
                    float x = (float)Math.Cos((randRoation + directionRotation.x) * (Math.PI / 180));
                    float y = (float)Math.Sin((randRoation + directionRotation.y) * (Math.PI / 180));
                    Vector2 newDirection = new Vector2(x, y);
                    rb.linearVelocity = newDirection * bulletSpeed;
                }

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
