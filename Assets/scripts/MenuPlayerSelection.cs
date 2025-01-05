
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuPlayerSelection : MonoBehaviour
{
   private int index;
   [SerializeField] private Image image;
   [SerializeField] private TextMeshProUGUI nametext;
   private GameManager gameManager;

   private void Start()
   {
    gameManager=GameManager.Instance;
    index=PlayerPrefs.GetInt("PlayerIndex");
    if(index>gameManager.characters.Count-1)
    {
        index=0;
    }
    ShowScreen();

   }

    private void ShowScreen()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite=gameManager.characters[index].pimage;
        nametext.text=gameManager.characters[index].pname;
    }
    public void Next()
    {
        if(index==gameManager.characters.Count-1)
        {
            index=0;
        }
        else
        {
            index++;
        }
        ShowScreen();
    }
      public void Previous()
    {
        if(index==0)
        {
            index=gameManager.characters.Count-1;
        }
        else
        {
            index--;
        }
        ShowScreen();
    }
    public void Select()
    {   
        
        SceneManager.LoadScene("MainScene");
    }

}
