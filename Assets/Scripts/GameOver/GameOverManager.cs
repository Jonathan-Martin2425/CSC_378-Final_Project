using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText; 

    [Header("Settings")]
    [SerializeField] private string gameSceneName = "Main"; 

    void Start() {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
            scoreText.text = "Score: " + finalScore;
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}