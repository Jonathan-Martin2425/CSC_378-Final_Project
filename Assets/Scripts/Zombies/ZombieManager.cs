using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [Header("Zombie Prefabs")]
    [SerializeField] GameObject RegularZombiePrefab;          // Regular zombie prefab
    [SerializeField] GameObject ConstructionZombiePrefab;         // Construction zombie prefab
    [SerializeField] GameObject soldierZombiePrefab;        // Soldier zombie prefab

    [Header("Spawn Settings")]
    [SerializeField] float spawnRate = 5f;
    [SerializeField] Vector2 innerBound = new Vector2(40f, 20f);
    [SerializeField] Vector2 outerBound = new Vector2(50f, 30f);

    [Header("Spawn Chances")]
    [Range(0f, 1f)]
    [SerializeField] float ConstructionSpawnChance = 0.2f;        // Chance for a Construction zombie.
    [Range(0f, 1f)]
    [SerializeField] float soldierSpawnChance = 0.1f;       // Chance for a soldier zombie.

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
        GameObject prefabToSpawn = RegularZombiePrefab; // Default to regular zombie.
        float roll = Random.value; // Random value between 0 and 1.

        // Check soldier spawn chance first.
        if (roll < soldierSpawnChance)
        {
            prefabToSpawn = soldierZombiePrefab;
        }
        // If not soldier, check for Construction.
        else if (roll < soldierSpawnChance + ConstructionSpawnChance)
        {
            prefabToSpawn = ConstructionZombiePrefab;
        }
        // Otherwise, default remains Regular.

        Instantiate(prefabToSpawn, GetSpawnPosition(), transform.rotation);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float randChoice1 = Random.Range(-1f, 1f);
        float randChoice2 = Random.Range(-1f, 1f);

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