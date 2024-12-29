using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    private GameObject[] list;
    private int index;
    void Start()
    {   
        index = PlayerPrefs.GetInt("Playerlist");

        list = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            list[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject x in list)
        {
            x.SetActive(false);
        }

        if (list[index]) list[index].SetActive(true);

    }

    public void Next()
    {
        list[index].SetActive(false);
        index++;
        if (index > list.Length - 1) index = 0;
        list[index].SetActive(true);
    }
    public void Previous()
    {
        list[index].SetActive(false);
        index--;
        if (index < 0) index = list.Length - 1;
        list[index].SetActive(true);
    }
    public void Select()
    {   
        PlayerPrefs.SetInt("Playerlist",index);
        SceneManager.LoadScene("MainScene");
    }

}
