using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Main"); 
    }

    // Called by the Credits button
    public void OnCreditsButtonPressed()
    {
        // SceneManager.LoadScene("CreditsScene"); 
    }
}