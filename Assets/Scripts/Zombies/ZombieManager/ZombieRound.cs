using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Round")]
public class ZombieRound : ScriptableObject
{
    public List<GameObject> zombiePrefabs;
    public List<int> zombieAmmounts;
    public float roundTimer;
    public float startDelay;
}
