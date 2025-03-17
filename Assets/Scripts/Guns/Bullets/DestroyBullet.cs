using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // This is set using the current weapon's bullet damage
    [SerializeField] protected float bulletDamage = 1f;
    [SerializeField] protected int level = 0;
    [SerializeField] protected bool isTrigger = false;
    public int numPierce = 1;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (TryGetComponent<Collider2D>(out var collider))
        {
            collider.isTrigger = true;
        }
    }

    public void setLevel(int level){
        this.level = level;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Bullet"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<ZombieBehavior>(out var zombie))
        {
            zombie.TakeDamage(bulletDamage);
            numPierce -= 1;
        }
        else
        {
            // If not, try SoldierZombieBehavior.
            var soldier = collision.gameObject.GetComponent<SoldierZombieBehavior>();
            if(soldier != null)
            {
                soldier.TakeDamage(bulletDamage);
                numPierce -= 1;
            }
        }

        if (numPierce < 1)
        {
            Destroy(gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     var zombie = collision.gameObject.GetComponent<ZombieBehavior>();
    //     if(zombie != null)
    //     {
    //         zombie.TakeDamage(bulletDamage);
    //         numPierce -= 1;
    //     }
    //     else
    //     {
    //         // If not, try SoldierZombieBehavior.
    //         var soldier = collision.gameObject.GetComponent<SoldierZombieBehavior>();
    //         if(soldier != null)
    //         {
    //             soldier.TakeDamage(bulletDamage);
    //             numPierce -= 1;
    //         }
    //     }
    //     if(!collision.gameObject.CompareTag("Bullet")
    //         && numPierce < 1)
    //     {
    //         Destroy(gameObject);
    //     }

    //     if(collision.gameObject.CompareTag("Tower"))
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }
}
