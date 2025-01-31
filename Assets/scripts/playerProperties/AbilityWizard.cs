

using Unity.VisualScripting;
using UnityEngine;

public class AbilityWizard : MonoBehaviour
{

    public int coolDown = 8;
    private int count;
    private TurnManager turn;
    public GameObject portalPrefab;
    public GameObject portalTargetPrefab;
    private GameObject portal;
    private GameObject portalTarget;
    private MazeLogic mazeR;
    [HideInInspector] public bool active = false;
    private Vector2 input;
    private Vector2 lastInput = Vector2.zero;
    [HideInInspector] public Vector2 targetPosition;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        mazeR = FindAnyObjectByType<MazeLogic>();

    }
    void Update()
    {
        //Requirements to be met to activate
        if (!active && Input.GetKeyDown(KeyCode.E) && !GetComponent<Move>().isMoving && GetComponent<Status>().abilityCoolDown == 0
        && gameObject == turn.currentPT && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause)
        {
            //Enter Selection Mode and Prepare the cursor
            active = true;
            GetComponent<Status>().selectionMode = true;
            targetPosition = transform.position;
            if (portalTarget == null)
                portalTarget = Instantiate(portalTargetPrefab, new Vector3(targetPosition.x, targetPosition.y, 0), Quaternion.identity);
            else portalTarget.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);

        }
        if (active)
        {
            ActiveAbility();
        }   
        //Logic to destroy the portal at the end of the turn but making sure that the player does not find himself in it
        if (portal != null && Input.GetKeyDown(KeyCode.Space) && !GetComponent<Move>().isMoving && !FindAnyObjectByType<UIMain>().isInPause)
        {
            mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] = 1;
            if (transform.position != portal.transform.position)
                Destroy(portal);
        }
        if (portal != null && mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] == 1
        && transform.position != portal.transform.position && !GetComponent<Move>().isMoving)
        {
            Destroy(portal);
        }

    }
    void ActiveAbility()
    {   
        //Ability function
        GetComponent<Move>().enabled = false;
        if (Input.GetKeyDown(KeyCode.Space) && !FindAnyObjectByType<UIMain>().isInPause)
        {
            active = false;
            GetComponent<Status>().selectionMode = false;
            Destroy(portalTarget);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !FindAnyObjectByType<UIMain>().isInPause)
        {
            GetComponent<Move>().enabled = true;
            active = false;
            GetComponent<Status>().selectionMode = false;
            Destroy(portalTarget);
            return;
        }
    //here use the input to select where the portal will be generated
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input != lastInput)
        {
            lastInput = input;
            if (input.x != 0 ^ input.y != 0)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                int x_ = (int)input.x;
                int y_ = (int)input.y;
                if (Check(targetPosition, input))
                    targetPosition += input;
                portalTarget.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);

            }
        }
        if (Input.GetKeyDown(KeyCode.E) && !FindAnyObjectByType<UIMain>().isInPause)
        {   
            //if it is confirmed, it generates the portal and enables the movement through it
            if (mazeR.GetValue((int)targetPosition.x, (int)targetPosition.y) == 1)
            {
                portal = Instantiate(portalPrefab, new Vector3(targetPosition.x, targetPosition.y, 0), Quaternion.identity);
                Destroy(portalTarget);
                mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] = 0;
                active = false;
                GetComponent<Status>().selectionMode = false;
                GetComponent<Move>().enabled = true;
                GetComponent<Status>().abilityCoolDown = coolDown;
            }
        }
    }
    private bool Check(Vector3 x, Vector3 y)
    {
        if (x + y == transform.position || x == transform.position) return true;
        return false;
    }
}
