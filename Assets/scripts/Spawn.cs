
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[] player=new GameObject[4];
    private Vector3[] position ={new Vector3(1,1,0),
                                 new Vector3(33,33,0),
                                 new Vector3(1,33,0),
                                 new Vector3(33,1,0)
                                };



    void Awake()
    {
        gameManager = GameManager.Instance;
        for (int x = 0; x < gameManager.playerCount; x++)
        {
            int indexList = PlayerPrefs.GetInt($"Player{x}Index");
            player[x]=Instantiate(GameManager.Instance.characters[indexList].player, position[x], Quaternion.identity);
            player[x].tag=$"Player{x+1}";
        }

    }


}
