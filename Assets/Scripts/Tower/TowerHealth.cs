using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TowerHealth : MonoBehaviour
{
    [Header("Tower Health Settings")]
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private float repairAmount = 20f;
    [SerializeField] private int repairCost = 50;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI towerHealthTMP; 
    
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [Header("Player Material Elements")]
    [SerializeField] private PlayerMats mats;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioSource hitSound;
    

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        //hitSound = GetComponent<AudioSource>();
        UpdateTowerHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        StartCoroutine(Flash(0.1f));
        hitSound.Play();
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

    IEnumerator Flash(float seconds)
    {
        spriteRenderer.color = Color.Lerp(originalColor, Color.red, 0.5f);
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
    }

    public void RepairTower()
    {
        if (mats.mats < repairCost)
        {
            Debug.Log("Not enough materials to repair tower");
            return;
        }
        
        if (currentHealth != maxHealth)
        {
            currentHealth += repairAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            mats.mats -= repairCost;
            UpdateTowerHealthUI();
            Debug.Log("Upgraded tower");
        }
        else
        {
            Debug.Log("Tower already at max health");
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