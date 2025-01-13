

using Unity.VisualScripting;
using UnityEngine;

public class AbilityMage : MonoBehaviour
{

    public int coolDown = 8;
    private int count;
    private TurnManager turn;
    public GameObject portalPrefab;
    public GameObject portalTargetPrefab;
    private GameObject portal;
    private GameObject portalTarget;
    private MazeLogic mazeR;
    private bool active = false;
    Vector2 input;
    Vector2 lastInput = Vector2.zero;
    public Vector2 targetPosition;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        mazeR = FindAnyObjectByType<MazeLogic>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !GetComponent<Move>().isMoving && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT)
        {
            active = true;
            targetPosition = transform.position;
            if (portalTarget == null)
                portalTarget = Instantiate(portalTargetPrefab, new Vector3(targetPosition.x, targetPosition.y, 0), Quaternion.identity);
            else portalTarget.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);

        }
        if (active)
        {
            ActiveAbility();
        }
        if (Input.GetKeyDown(KeyCode.Space) && portal != null)
        {
            mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] = 1;
            if (transform.position != portal.transform.position)
                Destroy(portal);
        }
        if(portal!=null && mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] == 1 && transform.position != portal.transform.position)
        {
            Destroy(portal);
        }

    }
    void ActiveAbility()
    {
        this.GetComponent<Move>().enabled = false;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.GetComponent<Move>().enabled = true;
            active = false;
            Destroy(portalTarget);
            return;
        }

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (mazeR.GetValue((int)targetPosition.x, (int)targetPosition.y) == 1)
            {
                portal = Instantiate(portalPrefab, new Vector3(targetPosition.x, targetPosition.y, 0), Quaternion.identity);
                Destroy(portalTarget);
                mazeR.maze[(int)targetPosition.x, (int)targetPosition.y] = 0;
                active = false;
                this.GetComponent<Move>().enabled = true;
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
