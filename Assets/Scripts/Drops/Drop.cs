using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Drop", menuName = "Drop")]
public class Drop : ScriptableObject
{
    public string dropName;
    public int relativeWeaponId;
    public int dropChance;
    public int dropCount;
    public Sprite sprite;
    
    public Drop(string name, int dropChance, int dropCount, Sprite sprite)
    {
        this.dropName = name;
        this.dropChance = dropChance;
        this.dropCount = dropCount;
        this.sprite = sprite;
    }
}