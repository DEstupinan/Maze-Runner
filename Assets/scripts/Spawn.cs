
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private MazeLogic maze;
    private GameObject[] player=new GameObject[4];
    



    void Awake()
    {   
        Vector3[] position ={new Vector3(1,1,0),
                                 new Vector3(maze.row-2,maze.col-2,0),
                                 new Vector3(1,maze.col-2,0),
                                 new Vector3(maze.row-2,1,0)
                                };
        gameManager = GameManager.Instance;
        for (int x = 0; x < gameManager.playerCount; x++)
        {
            int indexList = PlayerPrefs.GetInt($"Player{x}Index");
            player[x]=Instantiate(GameManager.Instance.characters[indexList].player, position[x], Quaternion.identity);
            player[x].tag=$"Player{x+1}";
        }

    }


}
