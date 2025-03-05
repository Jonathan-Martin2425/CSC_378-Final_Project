using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TowerHealth : MonoBehaviour
{
    [Header("Tower Health Settings")]
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI towerHealthTMP; 
    
    [SerializeField] private TextMeshProUGUI scoreTMP; 

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateTowerHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateTowerHealthUI();

        if (currentHealth <= 0) {
            if(scoreTMP != null) {
                string scoreStr = scoreTMP.text;
                if(scoreStr.StartsWith("Score: ")) {
                    scoreStr = scoreStr.Substring("Score: ".Length);
                    }
                int score;
                int.TryParse(scoreStr, out score);
                PlayerPrefs.SetInt("FinalScore", score);
            }
        SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateTowerHealthUI()
    {
        if (towerHealthTMP != null)
        {
            towerHealthTMP.text = "Health: " + currentHealth;
        }
    }
}