using UnityEngine;
using System.Collections;

public class ZombieBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform target;  
    [SerializeField] protected ZombieManager manager;  
    [Header("Attributes")]
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected float attackLength = 0.3f;
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected float acceleration = 5f;
    [SerializeField] protected float health = 3f;
    [SerializeField] protected float scoreVal = 10f;
    [SerializeField] protected float fireDuration = 2f;
    [SerializeField] protected float fireTickDamage = 1f;
    [SerializeField] protected int numFireTicks = 2;
    [SerializeField] protected ParticleSystem fireEffect;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected bool isAttacking = false;
    protected float distance = 0f;
    protected BoxCollider2D attackCollider;
    protected Color originalColor;
    protected bool isOnFire = false;
    protected bool isDead = false;
    public AudioSource zombieDeathSound;
    public AudioSource zombieHitSound;

    protected virtual void Start()
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
    protected virtual void Update()
    {
        if(rb.linearVelocity.magnitude > movementSpeed){
            Vector3 direction = rb.linearVelocity.normalized;
            rb.linearVelocity = direction * movementSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isAttacking) return;

        if (targetInRange())
            Attack();
        else
            Move();
    }
    
    protected bool targetInRange()
    {
        if (target == null) return false;
        distance = Vector3.Distance(transform.position, target.position);
        return distance <= attackRange;
    }

    protected void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    protected IEnumerator AttackRoutine()
    {
        isAttacking = true;
        attackCollider.enabled = true;
        yield return new WaitForSeconds(attackLength);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }

    protected virtual void Move()
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

    protected virtual void OnCollisionStay2D(Collision2D collision)
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

    public virtual void TakeDamage(float amount)
    {
        StartCoroutine(Flash(0.1f));
        health -= amount;
        if (health <= 0 && !isDead)
        {
            Instantiate(zombieDeathSound);
            Die();
            return;
        }
        zombieHitSound.Play();
    }

    public virtual void Die()
    {
        isDead = true;
        GetComponent<DropBag>().DropItem(transform.position);

        // Incrememnt Game Score here!!!
        GameObject.FindWithTag("UI").GetComponent<HudStats>().UpdateScore(scoreVal);

        manager.numZombiesAlive--;
        Destroy(gameObject);
    }

    protected IEnumerator Flash(float seconds)
    {
        spriteRenderer.color = Color.Lerp(originalColor, Color.red, 0.5f);
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
    }

    public virtual void setOnFire(){
        if(!isOnFire){
            StartCoroutine(takeFireDamage(fireDuration));
        }
    }

    protected IEnumerator takeFireDamage(float seconds){
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