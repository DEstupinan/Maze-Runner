using UnityEngine;

public class trapCooldown : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private bool used = false;
    [SerializeField] private int effect = 5;
    private int count;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {   
        //Requirements to be met to activate
        if (turn.currentPT.GetComponent<AbilityWarlock>() == null && turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && !used)
        {
            used = true;
            affected = turn.currentPT;
            GetComponent<SpriteRenderer>().enabled = true;
            if (!affected.GetComponent<Status>().abilityActive) affected.GetComponent<Status>().abilityCoolDown += effect;
            else affected.GetComponent<Status>().slot += effect;
            //The cooldown increases and if you have an active skill, the penalty is
            // in reserve and is applied when the skill ends
        }

        if (used && Input.GetKeyDown(KeyCode.Space) && !affected.GetComponent<Move>().isMoving && !FindAnyObjectByType<UIMain>().isInPause)
            Destroy(gameObject);
    }
}
