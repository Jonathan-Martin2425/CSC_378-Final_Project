using UnityEngine;

public class PlayerMats : MonoBehaviour
{
    [SerializeField] public int mats = 0;
    
    public void AddMats(int newMats){
        mats += newMats;
    }

    public void upgradeCost(int cost){
        mats -= cost;
        if(mats < 0){
            Debug.LogError("mats are negative, upgrade cost too much but still done");
        }
    }
}
