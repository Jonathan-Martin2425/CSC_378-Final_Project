using UnityEngine;
using System.Collections.Generic;

public class DropBag : MonoBehaviour
{
    public GameObject dropPrefab;
    public Drop[] dropList;
    public float dropForce;

    Drop GetDroppedItem()
    {
        List<Drop> possibleDrops = new List<Drop>();
        foreach (Drop drop in dropList)
        {
            if (Random.Range(0, 100) < drop.dropChance)
                possibleDrops.Add(drop);
        }

        if (possibleDrops.Count > 0)
            return possibleDrops[Random.Range(0, possibleDrops.Count)];

        Debug.Log("No items dropped! Sorry sucka!");
        return null;
    }

    public void DropItem(Vector3 spawnPos)
    {
        Drop droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            newDrop.GetComponent<DropBehavior>().InstantiateData(droppedItem);

            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            newDrop.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
}
