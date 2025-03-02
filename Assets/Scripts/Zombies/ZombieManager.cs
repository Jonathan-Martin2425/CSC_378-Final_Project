using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] float spawnRate = 5f;
    [SerializeField] float innerBound = 10f;
    [SerializeField] float outerBound = 20f;
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
        Debug.Log("Spawning a zombie");
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
                spawnPosition.x = Random.Range(-outerBound, -innerBound);
            else
                spawnPosition.x = Random.Range(innerBound, outerBound);
            spawnPosition.y = Random.Range(-outerBound, outerBound);
        }
        else
        {
            if (randChoice2 >= 0)
                spawnPosition.y = Random.Range(-outerBound, -innerBound);
            else
                spawnPosition.y = Random.Range(innerBound, outerBound);
            spawnPosition.x = Random.Range(-outerBound, outerBound);
        }
        return spawnPosition;
    }
}
