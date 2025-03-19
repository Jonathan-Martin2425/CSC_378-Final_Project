using UnityEngine;
using System.Collections;

public class SoldierZombieBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;       // Tower's transform.
    private TowerHealth towerHealth;                   // Reference to the TowerHealth script.
    ZombieManager manager; // used to decrement totalZombie counter when it dies

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float stopDistance = 5f;   // Distance at which the zombie stops moving.

    [Header("Attack Settings")]
    [SerializeField] private float fireRate = 1f;       // How often (in seconds) the zombie attacks.
    [SerializeField] private float directDamage = 5f;   // Damage dealt to the tower per attack.

    [Header("Health/Scoring")]
    [SerializeField] private float health = 3f;
    [SerializeField] private float scoreVal = 10f;

    [Header("Fire Effect Settings")]
    [SerializeField] private ParticleSystem fireEffect; // Particle system for fire effect.
    [SerializeField] private float fireDuration = 2f;     // Total duration of fire damage.
    [SerializeField] private float fireTickDamage = 1f;   // Damage per tick from fire.
    [SerializeField] private int numFireTicks = 2;        // Number of damage ticks during fire.
    private bool isOnFire = false;
    bool isDead = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float damageTimer = 0f;

    void Start()
    {
        // Find the tower if not manually assigned.
        if (target == null)
        {
            GameObject towerObj = GameObject.FindWithTag("Tower");
            if (towerObj != null)
                target = towerObj.transform;
        }
        
        // Get the TowerHealth component from the target.
        if (target != null)
            towerHealth = target.GetComponent<TowerHealth>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        manager = GameObject.FindWithTag("GameController").GetComponent<ZombieManager>();

        // Ensure the fire effect is not playing at start.
        if(fireEffect != null)
            fireEffect.Stop();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > stopDistance)
        {
            MoveTowardsTarget();
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop moving.
            DirectDamageTower();
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * acceleration * rb.mass);
        if (rb.linearVelocity.magnitude > movementSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * movementSpeed;
        }
        // Rotate to face the tower.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void DirectDamageTower()
    {
        damageTimer += Time.fixedDeltaTime;
        if (damageTimer >= fireRate)
        {
            damageTimer = 0f;
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(directDamage);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(Flash(0.1f));
        health -= amount;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
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
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
    }

    // Call this method to set the zombie on fire.
    public void SetOnFire()
    {
        if (!isOnFire)
        {
            StartCoroutine(TakeFireDamage(fireDuration));
        }
    }

    IEnumerator TakeFireDamage(float seconds)
    {
        isOnFire = true;
        if(fireEffect != null)
            fireEffect.Play();
        float tickInterval = seconds / numFireTicks;
        for (int i = 0; i < numFireTicks; i++)
        {
            yield return new WaitForSeconds(tickInterval);
            TakeDamage(fireTickDamage);
        }
        if(fireEffect != null)
            fireEffect.Stop();
        isOnFire = false;
    }
}