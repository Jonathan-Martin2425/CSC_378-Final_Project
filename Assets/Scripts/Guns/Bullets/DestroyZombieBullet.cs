using UnityEngine;

public class DestroyZombieBullet : DestroyBullet
{
    private PlayerController player;

    /*void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Debug.Log(player);
    }

    void Update()
    {
        if (player)
        {
            if(player.inTower){
                gameObject.layer = 6;
            }else{
                gameObject.layer = 0;
            }
        }
    }*/

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision.gameObject.tag);
        if (collision.CompareTag("Tower"))
        {
            TowerHealth towerHealth = collision.gameObject.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
            return;
        }

        // objects the bullet passes through
        if (collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.CompareTag("Drop"))
        {
            return;
        }

        if (collision.CompareTag("Zombie"))
        {
            return;
        }

        // any other object stops bullet
        Destroy(gameObject);
    }
}
