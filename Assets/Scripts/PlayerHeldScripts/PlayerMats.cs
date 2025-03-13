using UnityEngine;

public class PlayerMats : MonoBehaviour
{
    [SerializeField] public int mats = 0;
    
    public void AddMats(int newMats){
        mats += newMats;
        GameObject.FindWithTag("UI").GetComponent<InfoPanel>().UpdateInfo();
    }

    public void upgradeCost(int cost){
        mats -= cost;
        GameObject.FindWithTag("UI").GetComponent<InfoPanel>().UpdateInfo();
        if(mats < 0){
            Debug.LogError("mats are negative, upgrade cost too much but still done");
        }
    }
}
