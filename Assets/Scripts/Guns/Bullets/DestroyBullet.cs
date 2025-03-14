using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // This is set using the current weapon's bullet damage
    [SerializeField] protected float bulletDamage = 1f;
    [SerializeField] protected int level = 0;
    [SerializeField] protected bool isTrigger = false;

    public void setLevel(int level){
        this.level = level;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie") && !isTrigger)
        {
            collision.gameObject.GetComponent<ZombieBehavior>()
                .TakeDamage(bulletDamage);
        }

        if(!collision.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }
}
