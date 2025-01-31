using UnityEngine;

public class AbilityDarkEntity : MonoBehaviour
{
    public int coolDown = 1;

    private TurnManager turn;
    private MazeLogic mazeLogic;
    [SerializeField] private GameObject cruzPrefab;
    private GameObject cruz;

    private Move move;
    private bool aux;
    private bool active = false;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
    }
    void Update()
    {
        aux = true;
        if (!active && Input.GetKeyDown(KeyCode.E) && !move.isMoving
        && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT
        && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            cruz = Instantiate(cruzPrefab, transform.position, Quaternion.identity);
            active = true;
            GetComponent<Status>().abilityActive = true;
            aux = false;
        }
        if (active)
        {
            if (gameObject != turn.currentPT) cruz.SetActive(false);
            else cruz.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q) && gameObject == turn.currentPT && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                active = false;
                Destroy(cruz);
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().reserva;
                GetComponent<Status>().reserva = 0;
            }
            if (aux && Input.GetKeyDown(KeyCode.E) && gameObject == turn.currentPT && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                transform.position = cruz.transform.position;
                GetComponent<Move>().targetPosition = transform.position;
                active = false;
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().reserva;
                GetComponent<Status>().reserva = 0;
                Destroy(cruz);
            }
        }

    }
}
