using UnityEngine;
using TMPro;



public class HudStats : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public float totalScore = 0;

    void UpdateHealth()
    {

    }

    public void UpdateScore(float addedScore)
    {
        totalScore += addedScore;
        scoreText.text = "Score: " + totalScore;
    }

    public void UpdateAmmo(int currentAmmo, int reservedAmmo)
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + reservedAmmo;
    }
}
