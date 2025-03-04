using UnityEngine;
using TMPro;

public class HudStats : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;

    void UpdateHealth()
    {

    }

    void UpdateScore()
    {

    }

    public void UpdateAmmo()
    {
        Gun currentWeapon = player.GetComponent<PlayerWeaponController>().currentWeapon;
        ammoText.text = "Ammo: " + currentWeapon.currentAmmo + "/" + currentWeapon.magSize;
        // ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
    }
}
