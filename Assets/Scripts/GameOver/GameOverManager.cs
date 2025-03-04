using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText; 

    [Header("Settings")]
    [SerializeField] private string gameSceneName = "TowerCollisionScene"; 

    void Start()
    {
        string finalScoreString = PlayerPrefs.GetString("FinalScoreString", "Score: 0");
        scoreText.text = finalScoreString;
    }

    // This method is called by the Retry button OnClick event
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}