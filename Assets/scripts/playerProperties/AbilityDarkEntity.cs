using UnityEngine;

public class AbilityDarkEntity : MonoBehaviour
{
    public int coolDown = 1;

    private TurnManager turn;
    private MazeLogic mazeLogic;
    [SerializeField] private GameObject markPrefab;
    private GameObject mark;

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
        ////Requirements to be met to activate
        aux = true;
        if (!active && Input.GetKeyDown(KeyCode.E) && !move.isMoving
        && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT
        && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause)
        {
            mark = Instantiate(markPrefab, transform.position, Quaternion.identity);
            active = true;
            GetComponent<Status>().abilityActive = true;
            aux = false;
        }
        if (active)
        {   
            //logic to cancel the ability or to teleport to the mark
            if (gameObject != turn.currentPT) mark.SetActive(false);
            else mark.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q) && gameObject == turn.currentPT && !FindAnyObjectByType<UIMain>().isInPause)
            {
                active = false;
                Destroy(mark);
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().slot;
                GetComponent<Status>().slot = 0;
            }
            if (aux && Input.GetKeyDown(KeyCode.E) && gameObject == turn.currentPT && !FindAnyObjectByType<UIMain>().isInPause)
            {
                transform.position = mark.transform.position;
                GetComponent<Move>().targetPosition = transform.position;
                active = false;
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().slot;
                GetComponent<Status>().slot = 0;
                Destroy(mark);
            }
        }

    }
}
