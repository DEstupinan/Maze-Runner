using JetBrains.Annotations;
using UnityEngine;

public class AbilityHunter : MonoBehaviour
{
    public int coolDown = 4;
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
            mazeR.mazeObject[(int)transform.position.x, (int)transform.position.y] = trap = Instantiate(trapPrefab, transform.position, Quaternion.identity, mazeR.transform);
            used = true;
            mazeR.maze[(int)transform.position.x, (int)transform.position.y] = 3;

        }
        if (used && Input.GetKeyDown(KeyCode.Space) && !move.isMoving && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            used = false;
            trap.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}