using UnityEngine;

public class AbilitySpeed : MonoBehaviour
{
    public int coolDown = 4;
    public int power = 8;
    private TurnManager turn;
    private Move move;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT)
        {
            GetComponent<Status>().abilityCoolDown = coolDown;
            if (move.enabled)
            {
                move.moveAvailable += power;

            }
            else
            {   
                move.enabled=true;
                move.moveAvailable += power;
            }

        }

    }
}
