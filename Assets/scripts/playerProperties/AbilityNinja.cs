using UnityEngine;

public class AbilityNinja : MonoBehaviour
{
    public int coolDown = 2;

   [SerializeField] private Vector2 lastTarget;
   [SerializeField] private Vector2 direction;

    private TurnManager turn;
    private MazeLogic mazeLogic;
    
    [SerializeField] private GameObject clonPrefab;
    private GameObject clon;
   [SerializeField] private Vector2 clonTarget;
    


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
        if (!active && Input.GetKeyDown(KeyCode.E) && !move.isMoving
        && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT
        && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
           clon= Instantiate(clonPrefab, transform.position, Quaternion.identity);
            clonTarget = transform.position;
            lastTarget = move.targetPosition;
            active = true;
            GetComponent<Status>().abilityActive = true;
            aux = false;
        }
        if (active)
        {
            if (gameObject != turn.currentPT) clon.SetActive(false);
            else clon.SetActive(true);
            ClonMove();

            if (Input.GetKeyDown(KeyCode.Q) && gameObject == turn.currentPT && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                active = false;
                Destroy(clon);
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().reserva;
                GetComponent<Status>().reserva = 0;
            }
            if (aux && Input.GetKeyDown(KeyCode.E) && gameObject == turn.currentPT && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                transform.position = clon.transform.position;
                GetComponent<Move>().targetPosition = transform.position;
                active = false;
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().reserva;
                GetComponent<Status>().reserva = 0;
                Destroy(clon);
            }
        }

    }
    void ClonMove()
    {

        direction = move.targetPosition-lastTarget ;

        if (!GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause
        && mazeLogic.maze[(int)clonTarget.x - (int)direction.x, (int)clonTarget.y - (int)direction.y] != 1
        && mazeLogic.maze[(int)clonTarget.x - (int)direction.x, (int)clonTarget.y - (int)direction.y] != -1)
        {
            clonTarget -= direction;
            clon.transform.position = Vector2.MoveTowards(clon.transform.position, clonTarget, move.speed * Time.deltaTime);
        }

        lastTarget = move.targetPosition;

    }
}