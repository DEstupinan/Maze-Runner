using UnityEngine;

public class AbilityMinotaur : MonoBehaviour
{
    public int coolDown = 4;
    public int power = 10;
    private TurnManager turn;
    private Move move;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0
        && gameObject == turn.currentPT && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause)
        {
            GetComponent<Status>().abilityCoolDown = coolDown;
            if(!move.enabled)move.enabled=true;
            if (GetComponent<Status>().paralysis)GetComponent<Status>().paralysis=false;
            
                move.moveAvailable += power;


        }

    }
}
