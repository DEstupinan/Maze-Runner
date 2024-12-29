using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MenuSelection");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    
    public void Exit()
    {
        
        Application.Quit();
    }
}
