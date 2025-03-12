using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] float spawnRate = 5f;
    [SerializeField] Vector2 innerBound = new Vector2(40f, 20f);
    [SerializeField] Vector2 outerBound = new Vector2(50f, 30f);
    static ZombieManager _instance;

    public static ZombieManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ZombieManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("ZombieManager");
                    _instance = go.AddComponent<ZombieManager>();
                }
            }
            return _instance;
        }
    }  

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        InvokeRepeating("Spawn", 0, spawnRate);
    }

    void Spawn()
    {
        GameObject zombie = Instantiate(zombiePrefab, GetSpawnPosition(), transform.rotation);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        float randChoice1 = Random.Range(-1, 1);
        float randChoice2 = Random.Range(-1, 1);

        if (randChoice1 >= 0)
        {
            if (randChoice2 >= 0)
                spawnPosition.x = Random.Range(-outerBound.x, -innerBound.x);
            else
                spawnPosition.x = Random.Range(innerBound.x, outerBound.x);
            spawnPosition.y = Random.Range(-outerBound.y, outerBound.y);
        }
        else
        {
            if (randChoice2 >= 0)
                spawnPosition.y = Random.Range(-outerBound.y, -innerBound.y);
            else
                spawnPosition.y = Random.Range(innerBound.y, outerBound.y);
            spawnPosition.x = Random.Range(-outerBound.x, outerBound.x);
        }
        return spawnPosition;
    }
}
