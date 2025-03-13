using UnityEngine;
using UnityEngine.UI;
using System;

public class GunButton : MonoBehaviour
{
    public GameObject player;
    public Gun gun;

    public void GunSelected()
    {
        Debug.Log("switched by UI");
        player.GetComponent<PlayerWeaponController>().SwitchGun(gun.id);
    }
}
