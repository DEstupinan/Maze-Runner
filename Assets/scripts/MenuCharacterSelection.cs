
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuCharacterSelection : MonoBehaviour
{
    private int[] index;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nametext;
    [SerializeField] private TextMeshProUGUI playerNumber;
    private GameManager gameManager;
    private int i = 0;

    private void Start()
    {
        gameManager = GameManager.Instance;
        index = new int[gameManager.playerCount];
        index[i] = PlayerPrefs.GetInt($"Player{i}Index");
        if (index[i] > gameManager.characters.Count - 1)
        {
            index[i] = 0;
        }
        ShowScreen();

    }

    private void ShowScreen()
    {
        PlayerPrefs.SetInt($"Player{i}Index", index[i]);
        image.sprite = gameManager.characters[index[i]].pimage;
        nametext.text = gameManager.characters[index[i]].pname;
        playerNumber.text=$"Jugador{i+1}";
    }
    public void Back()
    {
        SceneManager.LoadScene("PlayersNumber");
    }
    public void Next()
    {
        if (index[i] == gameManager.characters.Count - 1)
        {
            index[i] = 0;
        }
        else
        {
            index[i]++;
        }
        ShowScreen();
    }
    public void Previous()
    {
        if (index[i] == 0)
        {
            index[i] = gameManager.characters.Count - 1;
        }
        else
        {
            index[i]--;
        }
        ShowScreen();
    }
    public void Select()
    {
        i++;
        if (i == gameManager.playerCount)
            SceneManager.LoadScene("MainScene");
        else
            ShowScreen();
    }

}
