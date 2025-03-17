using UnityEngine;
using System.Collections;

public class SniperRifle : Gun
{
    public int pierce = 1;
    public bool isRailgun = false;
    public GameObject laserPrefab;
    public AudioSource railgunFire;
    public AudioSource railgunCharge;

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
            if (isRailgun && railgunCharge)
            {
                railgunCharge.Play();
            }
        }
    }

    private void FireBullet(Vector2 direction)
    {
        if (isRailgun)
        {
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            StartCoroutine(LaserFade(laser, 0.2f));

            railgunFire.Play();
            currentAmmo -= 1;

            if (currentAmmo < 0)
            {
                currentAmmo = 0;
            }
            
            return;
        }
        fireSound.Play();
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
    
    private IEnumerator LaserFade(GameObject laser, float seconds)
    {
        SpriteRenderer spriteRenderer = laser.transform.Find("Sprite")
            .GetComponent<SpriteRenderer>();

        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = new Color(
                originalColor.r, 
                originalColor.g,
                originalColor.b, 
                Mathf.Lerp(originalColor.a, 0f, elapsedTime / seconds));
            yield return null;
        }

        spriteRenderer.color = new Color(
            originalColor.r, 
            originalColor.g,
            originalColor.b, 
            0f);

        Destroy(laser);
    }
}
