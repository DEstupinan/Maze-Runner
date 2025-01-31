using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPlayerNumber : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void P2()
    {
        gameManager.playerCount = 2;
        SceneManager.LoadScene("CharacterSelection");
    }
    public void P3()
    {
        gameManager.playerCount = 3;
        SceneManager.LoadScene("CharacterSelection");
    }

    public void P4()
    {
        gameManager.playerCount = 4;
        SceneManager.LoadScene("CharacterSelection");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
