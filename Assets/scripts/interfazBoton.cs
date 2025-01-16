using UnityEngine;
using UnityEngine.SceneManagement;

public class interfazBoton : MonoBehaviour
{
    public GameObject status;
    public GameObject controls;
    public GameObject pause;
    public bool isInPause = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
    public void ChangeStatus()
    {
        if (!status.activeSelf)
        {
            status.SetActive(true);
        }
        else status.SetActive(false);
    }
    public void ChangeControls()
    {
        if (!controls.activeSelf)
        {
            controls.SetActive(true);
        }
        else controls.SetActive(false);
    }
    public void Pause()
    {

        if (!pause.activeSelf)
        {
            isInPause = true;
            Time.timeScale = 0f;
            pause.SetActive(true);
        }
        else
        {
            isInPause = false;
            Time.timeScale = 1f;
            pause.SetActive(false);
        }
    }
    public void Reanudar()
    {
        isInPause = false;
        Time.timeScale = 1f;
        pause.SetActive(false);
    }
    public void Reiniciar()
    {   
        isInPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void cambiarP()
    {
        SceneManager.LoadScene("PlayerSelection");
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void MenuP()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
