using UnityEngine;

public class DestroyPellet : DestroyBullet
{
    public float knockback = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            if(level >= 1){
                Vector2 force = GetComponent<Rigidbody2D>().linearVelocity * knockback;
                Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
                otherRb.AddForce(force * otherRb.mass);
            }
            ZombieBehavior zombie = collision.gameObject.GetComponent<ZombieBehavior>();
            if(level >= 3){
                zombie.setOnFire();
            }
            
            zombie.TakeDamage(bulletDamage);
        }

        if(!collision.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }

}
