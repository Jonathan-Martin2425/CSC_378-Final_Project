using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    public float magnetRange = 5f;
    public float speed = 1f;
    public float dropForce = 300f;
    Transform player;
    Rigidbody2D rb;
    PlayerWeaponController weaponController;
    float playerDistance;
    int dropCount;

    void Update()
    {
        playerDistance = (transform.position - player.position).magnitude;
        if (playerDistance <= magnetRange)
            Magnetize();
    }

    void Awake()
    {
        if (!player)
            player = GameObject.FindWithTag("Player").transform;
        if (!weaponController)
            weaponController = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>();
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
    }

    public void InstantiateData(Drop drop)
    {
        dropCount = drop.dropCount;
        GetComponent<SpriteRenderer>().sprite = drop.sprite;

        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player picked up a drop");
        if (other.gameObject.CompareTag("Player"))
        {
            weaponController.AddAmmo(dropCount);
            Debug.Log("Drop Destroyed");
            // add to inventory here
            Destroy(gameObject);
        }
    }


    void Magnetize()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, magnetRange/playerDistance * speed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }
}
