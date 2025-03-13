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
    public static bool isPaused = false;

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
        isPaused = !isPaused;

        if (isPaused)
        {
            sidebar.SetActive(true);
            if (infoUpgradePanel)
                infoUpgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            sidebar.SetActive(false);
            if (infoUpgradePanel)
                infoUpgradePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
