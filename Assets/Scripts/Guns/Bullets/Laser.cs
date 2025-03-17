using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float bulletDamage = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (TryGetComponent<Collider2D>(out var collider))
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            //Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Bullet"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<ZombieBehavior>(out var zombie))
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
    }

    public void SetDamage(float damage)
    {
        Debug.Log("Set damage: " + damage);
        bulletDamage = damage;
    }
}
