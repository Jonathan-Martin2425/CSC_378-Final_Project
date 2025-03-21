using UnityEngine;
using UnityEngine.UI;
using System;

public class GunButton : MonoBehaviour
{
    public GameObject player;
    public Gun gun;

    public void SelectGun()
    {
        if (WeaponInfoController.isPaused)
        {
            return;
        }
        
        player.GetComponent<PlayerWeaponController>().SwitchGun(gun.id);
    }
}
