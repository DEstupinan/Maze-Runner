using UnityEngine;

public class AbilityKunnka : MonoBehaviour
{
    public int coolDown = 4;

    private TurnManager turn;
    private MazeLogic mazeLogic;
    [SerializeField] private GameObject cruzPrefab;
    private GameObject cruz;
    [SerializeField] private GameObject roadKPrefab;
    private GameObject roadK;
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
        if (!active && Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT)
        {
            cruz = Instantiate(cruzPrefab, transform.position, Quaternion.identity);
            active = true;
            aux = false;
        }
        if (active)
        {
            if (gameObject != turn.currentPT) cruz.SetActive(false);
            else cruz.SetActive(true);
            if (aux && Input.GetKeyDown(KeyCode.E))
            {
                transform.position = cruz.transform.position;
                GetComponent<Move>().targetPosition = transform.position;
                active = false;
                GetComponent<Status>().abilityCoolDown = coolDown;
                Destroy(cruz);
            }
        }

    }
}
