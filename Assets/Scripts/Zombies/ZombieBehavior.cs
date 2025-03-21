using UnityEngine;
using System.Collections;

public class ZombieBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform target;  
    [SerializeField] ZombieManager manager;  
    [Header("Attributes")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackLength = 0.3f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float health = 3f;
    [SerializeField] float scoreVal = 10f;
    [SerializeField] float fireDuration = 2f;
    [SerializeField] float fireTickDamage = 1f;
    [SerializeField] int numFireTicks = 2;
    [SerializeField] ParticleSystem fireEffect;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    bool isAttacking = false;
    float distance = 0f;
    BoxCollider2D attackCollider;
    Color originalColor;
    bool isOnFire = false;
    bool isDead = false;
    public AudioSource zombieDeathSound;
    public AudioSource zombieHitSound;

    void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Tower").transform;
        
        attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;

        manager = GameObject.FindWithTag("GameController").GetComponent<ZombieManager>();
        fireEffect.Stop();
    }

    // to prevent insane knockback
    void Update()
    {
        if(rb.linearVelocity.magnitude > movementSpeed){
            Vector3 direction = rb.linearVelocity.normalized;
            rb.linearVelocity = direction * movementSpeed;
        }
    }

    void FixedUpdate()
    {
        if (isAttacking) return;

        if (targetInRange())
            Attack();
        else
            Move();
    }
    
    bool targetInRange()
    {
        if (target == null) return false;
        distance = Vector3.Distance(transform.position, target.position);
        return distance <= attackRange;
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        attackCollider.enabled = true;
        yield return new WaitForSeconds(attackLength);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }

    void Move()
    {
        //Debug.Log("Zombie is moving");
        if (target == null) return;

        //get direction of target
        Vector3 direction = target.position - transform.position;

        //make zombie face target by setting rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        // adds force/acceleration and setting to max speed if velocity
        // goes over movementSpeed
        direction.Normalize();
        rb.AddForce(direction * acceleration * rb.mass); // mass normalizes knockback
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower") && !isAttacking)
        {
            TowerHealth towerHealth = collision.gameObject.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackDamage);
            }
            Attack();
        }
    }

    public void TakeDamage(float amount)
    {
        zombieHitSound.Play();
        StartCoroutine(Flash(0.1f));
        health -= amount;
        if (health <= 0 && !isDead)
        {
            Instantiate(zombieDeathSound);
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        GetComponent<DropBag>().DropItem(transform.position);

        // Incrememnt Game Score here!!!
        GameObject.FindWithTag("UI").GetComponent<HudStats>().UpdateScore(scoreVal);

        manager.numZombiesAlive--;
        Destroy(gameObject);
    }

    IEnumerator Flash(float seconds)
    {
        spriteRenderer.color = Color.Lerp(originalColor, Color.red, 0.5f);
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
    }

    public void setOnFire(){
        if(!isOnFire){
            StartCoroutine(takeFireDamage(fireDuration));
        }
    }

    IEnumerator takeFireDamage(float seconds){
        isOnFire = true;
        fireEffect.Play();
        float tickInterval = seconds / numFireTicks;
        for(int i = 0; i < numFireTicks; i++){
            yield return new WaitForSeconds(tickInterval);
            TakeDamage(fireTickDamage);
        }
        fireEffect.Stop();
        isOnFire = false;

    }

    // IEnumerator PlayDeathSound()
    // {
        
    // }
}
