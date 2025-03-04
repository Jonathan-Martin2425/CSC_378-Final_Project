using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    public Transform player;
    public int dropCount;
    public float magnetRange = 5f;
    public float speed = 10f;
    float playerDistance;
    Rigidbody2D rb;

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

        rb = GetComponent<Rigidbody2D>();
    }

    public void InstantiateData(Drop drop)
    {
        dropCount = drop.dropCount;
        GetComponent<SpriteRenderer>().sprite = drop.sprite;
    }
    
    void OnTrigger2DEnter(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
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
