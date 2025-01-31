using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{   
    public GameObject enciclopedia;
    public TMP_Text description;
    public void Play()
    {
        SceneManager.LoadScene("PlayersNumber");
    }
    public void Enciclopedia()
    {   
        description.text = "";
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
