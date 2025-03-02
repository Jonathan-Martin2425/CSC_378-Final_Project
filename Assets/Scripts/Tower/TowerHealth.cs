using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TowerHealth : MonoBehaviour
{
    [Header("Tower Health Settings")]
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;

    [Header("UI Elements")]
    [SerializeField] private Slider healthBar;  

    private bool isDead = false;

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Initialize health bar if assigned
        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;
    }

    private void Update()
    {
        // Change it to decremtn upon collision with zombie prefab
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;

        if (currentHealth <= 0f && !isDead)
        {
            isDead = true;
            StartCoroutine(DelayedSceneReload(1.5f));
        }
    }

    private IEnumerator DelayedSceneReload(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}