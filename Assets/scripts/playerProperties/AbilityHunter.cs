using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityHunter : MonoBehaviour
{
    public int coolDown = 1;
    int x;
    int y;
    [SerializeField] private GameObject trapPrefab;
    private GameObject trap;
    private TurnManager turn;
    private MazeLogic mazeR;
    private bool used = false;
    private Move move;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
        mazeR = FindAnyObjectByType<MazeLogic>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0
        && gameObject == turn.currentPT && mazeR.maze[(int)transform.position.x, (int)transform.position.y] == 0
        && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            GetComponent<Status>().abilityCoolDown = coolDown;
            while (true)
            {


                x = Random.Range(1, mazeR.col - 1);
                y = Random.Range(1, mazeR.row - 1);
                if (mazeR.maze[x, y] == 0)
                {
                    trap = Instantiate(trapPrefab, new Vector3(x, y, 0), Quaternion.identity, mazeR.transform);
                    break;
                }
            }

            used = true;
            mazeR.maze[x, y] = 3;

        }
        if (used && Input.GetKeyDown(KeyCode.Space) && !move.isMoving && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            used = false;
            trap.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}