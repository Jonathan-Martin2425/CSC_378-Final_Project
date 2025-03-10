using UnityEngine;
using System.Collections;

public class ZombieBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform target;  
    [Header("Attributes")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackLength = 0.3f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float health = 3f;
    [SerializeField] float scoreVal = 10f;
    private SpriteRenderer spriteRenderer;

    bool isAttacking = false;
    float distance = 0f;
    BoxCollider2D attackCollider;

    void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Tower").transform;
        
        attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log("Zombie attacked the tower");
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
        Debug.Log("Zombie is moving");
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            movementSpeed * Time.fixedDeltaTime
        );
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower") && !isAttacking)
        {
            TowerHealth towerHealth = collision.gameObject.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackDamage);
            }
        }
    }*/

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
        StartCoroutine(Flash(0.1f));
        health -= amount;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        GetComponent<DropBag>().DropItem(transform.position);

        // Incrememnt Game Score here!!!
        GameObject.FindWithTag("UI").GetComponent<HudStats>().UpdateScore(scoreVal);

        Destroy(gameObject);
    }

    IEnumerator Flash(float seconds)
    {
        Color orignalColor = spriteRenderer.color;

        spriteRenderer.color = Color.Lerp(orignalColor, Color.red, 0.5f);
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = orignalColor;
    }
}
