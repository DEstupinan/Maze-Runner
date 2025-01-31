using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public GameObject encyclopedia;
    public TMP_Text description;
    public void Play()
    {
        SceneManager.LoadScene("PlayersNumber");
    }
    public void Encyclopedia()
    {
        description.text = "";
        encyclopedia.SetActive(true);
    }

    public void Exit()
    {

        Application.Quit();
    }
    public void Close()
    {
        encyclopedia.SetActive(false);
    }
}
