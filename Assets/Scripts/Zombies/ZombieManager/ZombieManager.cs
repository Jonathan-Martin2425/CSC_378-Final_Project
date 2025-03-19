using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] List<ZombieRound> rounds;
    [SerializeField] int curRound = 0;

    [Header("Spawn Settings")]
    [SerializeField] Vector2 innerBound = new Vector2(40f, 20f);
    [SerializeField] Vector2 outerBound = new Vector2(50f, 30f);
    public float curRoundTime = 0;
    public int numZombiesAlive = 0;
    public bool startDelay = true; //EOR stands for End of Round

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
    }

    void Update()
    {
        if(curRoundTime <= 0 || (numZombiesAlive <= 0 && !startDelay)){
            Spawn();
        }
        curRoundTime -= Time.deltaTime;
    }

    void Spawn()
    {

        // initializes start delay for a round
        if(!startDelay){
            curRoundTime = rounds[curRound].startDelay;
            startDelay = true;
            return;
        }
        startDelay = false;


        //spawns all zombies in round
        ZombieRound round = rounds[curRound];
        for(int i = 0; i < round.zombiePrefabs.Count; i++){
            for(int j = 0; j < round.zombieAmmounts[i]; j++){
                numZombiesAlive++;
                Instantiate(round.zombiePrefabs[i], GetSpawnPosition(), transform.rotation);
            }
        }

        //set round timer before next wave spawns
        curRoundTime = rounds[curRound].roundTimer;

        //increament round if it isn't the last one
        if(curRound < rounds.Count - 1){
            curRound++;
        }
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