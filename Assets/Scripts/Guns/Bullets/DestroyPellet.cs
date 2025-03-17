using UnityEngine;

public class DestroyPellet : DestroyBullet
{
    public float knockback = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            if (level >= 1)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 force = rb.linearVelocity * knockback;
                    Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (otherRb != null)
                    {
                        otherRb.AddForce(force * otherRb.mass);
                    }
                }
            }
            
            ZombieBehavior zombie = collision.gameObject.GetComponent<ZombieBehavior>();
            if (zombie != null)
            {
                if (level >= 3)
                {
                    zombie.setOnFire();
                }
                zombie.TakeDamage(bulletDamage);
            }
            else
            {
                SoldierZombieBehavior soldier = collision.gameObject.GetComponent<SoldierZombieBehavior>();
                if (soldier != null)
                {
                    if (level >= 3)
                    {
                        soldier.SetOnFire();
                    }
                    soldier.TakeDamage(bulletDamage);
                }
            }
        }

        if (!collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}