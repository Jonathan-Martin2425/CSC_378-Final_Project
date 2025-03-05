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

    public void UpdateAmmo()
    {
        Gun currentWeapon = player.GetComponent<PlayerWeaponController>().currentWeapon;
        ammoText.text = "Ammo: " + currentWeapon.currentAmmo + "/" + currentWeapon.magSize;
    }
}
