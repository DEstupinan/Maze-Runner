using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMain : MonoBehaviour
{
    public GameObject status;
    public GameObject controls;
    public GameObject pause;
    public MazeLogic mazeLogic;
    public GameObject end;
    public GameObject lightG;
    public CameraFollow cameraFollow;
    private string winnerTag;
    private string winnerName;
    private bool finished = false;
    public bool isInPause = false;
    [SerializeField] private TMP_Text winnerText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
    //UI function of the main scene (its buttons and interface update)
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
            if (!finished)
            {
                isInPause = false;
                Time.timeScale = 1f;
            }
            pause.SetActive(false);
        }
    }
    public void Resume()
    {
        if (!finished)
        {
            isInPause = false;
            Time.timeScale = 1f;
        }
        pause.SetActive(false);
    }
    public void Retry()
    {
        isInPause = false;
        Time.timeScale = 1f;
        finished = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeP()
    {
        isInPause = false;
        Time.timeScale = 1f;
        finished = false;
        SceneManager.LoadScene("CharacterSelection");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        isInPause = false;
        Time.timeScale = 1f;
        finished = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void End()
    {

        finished = true;
        end.SetActive(true);
        cameraFollow.GetComponent<CameraFollow>().enabled = false;
        cameraFollow.transform.position = new Vector3(mazeLogic.col / 2, mazeLogic.row / 2, -10);
        cameraFollow.GetComponent<Camera>().orthographicSize = 18;
        lightG.SetActive(true);
        winnerTag = FindAnyObjectByType<Treasure>().GetComponent<Treasure>().winnerN;
        switch (winnerTag)
        {
            case "Player1": winnerName = "Jugador1"; break;
            case "Player2": winnerName = "Jugador2"; break;
            case "Player3": winnerName = "Jugador3"; break;
            case "Player4": winnerName = "Jugador4"; break;

            default: break;
        }
        winnerText.text = $"El {winnerName} ha ganado !!!";
        isInPause = true;
        Time.timeScale = 0f;
    }
    public void SeeMap()
    {
        end.SetActive(false);
    }
}
