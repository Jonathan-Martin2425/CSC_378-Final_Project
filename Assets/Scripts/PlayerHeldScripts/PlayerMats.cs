using UnityEngine;

public class PlayerMats : MonoBehaviour
{
    [SerializeField] public int mats = 0;
    [SerializeField] public int maxMats = 999;
    
    public void AddMats(int newMats){
        if (mats + newMats > maxMats)
            mats = maxMats;
        else
            mats += newMats;
    }

    public void UpgradeCost(int cost){
        mats -= cost;
        if(mats < 0){
            Debug.LogError("mats are negative, upgrade cost too much but still done");
        }
    }
}
