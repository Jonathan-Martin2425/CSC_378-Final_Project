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
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieBehavior>()
                .TakeDamage(bulletDamage);
        }
    }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }
}
