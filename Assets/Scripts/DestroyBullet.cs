using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // This is set using the current weapon's bullet damage
    [SerializeField] private float bulletDamage = 1f;

    void OnBecameInvisible()
    {
        Destroy(gameObject);        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieBehavior>()
                .TakeDamage(bulletDamage);
        }

        if(!collision.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            collision.GetComponent<ZombieBehavior>()
                .TakeDamage(bulletDamage);
        }

        if(!collision.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }
}
