using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // This is set using the current weapon's bullet damage
    [SerializeField] protected float bulletDamage = 1f;
    [SerializeField] protected int level = 0;
    [SerializeField] protected bool isTrigger = false;
    [SerializeField] private int numPierce = 1;

    public void setLevel(int level){
        this.level = level;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var zombie = collision.gameObject.GetComponent<ZombieBehavior>();
        if(zombie != null)
        {
            zombie.TakeDamage(bulletDamage);
        }
        else
        {
            // If not, try SoldierZombieBehavior.
            var soldier = collision.gameObject.GetComponent<SoldierZombieBehavior>();
            if(soldier != null)
            {
                soldier.TakeDamage(bulletDamage);
            }
        }
        if(!collision.gameObject.CompareTag("Bullet"))
        {
        Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }
}
