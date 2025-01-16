using UnityEngine;
using UnityEngine.SceneManagement;

public class final : MonoBehaviour
{

    void Start()
    {
        Invoke("loadmenu", 4);
    }

    public void loadmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
