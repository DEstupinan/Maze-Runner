using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public GameObject enciclopedia;
    public void Play()
    {
        SceneManager.LoadScene("PlayersNumber");
    }
    public void Enciclopedia()
    {
        enciclopedia.SetActive(true);
    }

    public void Exit()
    {

        Application.Quit();
    }
    public void Cerrar()
    {
        enciclopedia.SetActive(false);
    }
}
