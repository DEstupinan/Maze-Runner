
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private MazeLogic maze;
    private GameObject[] player = new GameObject[4];




    void Awake()
    {
        List<Vector3> position = new List<Vector3>{new Vector3(1,1,0),
                                 new Vector3(maze.row-2,maze.col-2,0),
                                 new Vector3(1,maze.col-2,0),
                                 new Vector3(maze.row-2,1,0)
                                };
        gameManager = GameManager.Instance;

        for (int x = 0; x < gameManager.playerCount; x++)
        {   
            // instantiate players in random corners and assign them their tag
            int indexList = PlayerPrefs.GetInt($"Player{x}Index");
            int spawnPosition = Random.Range(0, position.Count);
            player[x] = Instantiate(GameManager.Instance.characters[indexList].player, position[spawnPosition], Quaternion.identity);

            position.RemoveAt(spawnPosition);
            player[x].tag = $"Player{x + 1}";
        }

    }


}
