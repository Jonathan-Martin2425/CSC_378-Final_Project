using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Main"); 
    }

    public void OnCreditsButtonPressed()
    {
        SceneManager.LoadScene("Credits"); 
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("TitleScene");
    }
}