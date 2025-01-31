using UnityEngine;

public class AbilityGolem : MonoBehaviour
{
    public int coolDown = 2;

    private Vector2 lastTarget;
    private Vector2 direction;

    private TurnManager turn;
    private MazeLogic mazeLogic;

    [SerializeField] private GameObject clonePrefab;
    private GameObject clone;
    private Vector2 cloneTarget;



    private Move move;
    private bool aux;
    private bool active = false;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
        mazeLogic = FindAnyObjectByType<MazeLogic>();
    }
    void Update()
    {
        aux = true;
        //Requirements to be met to activate
        if (!active && Input.GetKeyDown(KeyCode.E) && !move.isMoving
        && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT
        && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause)
        {
            //Create the minigolem in the current tile
            clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            cloneTarget = transform.position;
            lastTarget = move.targetPosition;
            active = true;
            GetComponent<Status>().abilityActive = true;
            aux = false;
        }
        if (active)
        {
            //logic to cancel the ability or to teleport to the minigolem
            if (gameObject != turn.currentPT) clone.SetActive(false);
            else clone.SetActive(true);
            cloneMove();

            if (Input.GetKeyDown(KeyCode.Q) && gameObject == turn.currentPT && !FindAnyObjectByType<UIMain>().isInPause)
            {
                active = false;
                Destroy(clone);
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().slot;
                GetComponent<Status>().slot = 0;
            }
            if (aux && Input.GetKeyDown(KeyCode.E) && gameObject == turn.currentPT && !FindAnyObjectByType<UIMain>().isInPause)
            {
                transform.position = clone.transform.position;
                GetComponent<Move>().targetPosition = transform.position;
                active = false;
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().slot;
                GetComponent<Status>().slot = 0;
                Destroy(clone);
            }
        }

    }
    void cloneMove()
    {
        //Method to enable the movement of the minigolem in the opposite direction of the golem
        direction = move.targetPosition - lastTarget;

        if (!GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause
        && mazeLogic.maze[(int)cloneTarget.x - (int)direction.x, (int)cloneTarget.y - (int)direction.y] != 1
        && mazeLogic.maze[(int)cloneTarget.x - (int)direction.x, (int)cloneTarget.y - (int)direction.y] != -1)
        {
            cloneTarget -= direction;
            clone.transform.position = Vector2.MoveTowards(clone.transform.position, cloneTarget, move.speed * Time.deltaTime);
        }

        lastTarget = move.targetPosition;

    }
}