using UnityEngine;
using UnityEngine.UI;
using System;

public class GunButton : MonoBehaviour
{
    public GameObject player;
    public Gun gun;

    public void GunSelected()
    {
        player.GetComponent<PlayerWeaponController>().SwitchGun(gun.id);
    }
}
