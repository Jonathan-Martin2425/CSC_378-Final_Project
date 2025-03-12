using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Collections.Generic;

public class WeaponInfoController : MonoBehaviour
{
    public GameObject sidebar;
    public GameObject infoUpgradePanel;
    // change this back to false after done testing
    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInfoScreen();
        }
    }

    void ToggleInfoScreen()
    {
        isActive = !isActive;

        if (isActive)
        {
            sidebar.SetActive(true);
            infoUpgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            sidebar.SetActive(false);
            infoUpgradePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
