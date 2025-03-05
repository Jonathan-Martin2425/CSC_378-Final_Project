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
        PlayerWeaponController weaponController = player.GetComponent<PlayerWeaponController>();
        ammoText.text = "Ammo: " + weaponController.currentWeapon.currentAmmo + "/" + weaponController.GetCurrentReservedAmmo();
    }
}
