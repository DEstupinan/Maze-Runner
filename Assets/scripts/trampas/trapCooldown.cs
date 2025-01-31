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
        if (turn.currentPT.GetComponent<AbilityWarlock>() == null && turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && !used)
        {
            used = true;
            affected = turn.currentPT;
            GetComponent<SpriteRenderer>().enabled = true;
            if (!affected.GetComponent<Status>().abilityActive) affected.GetComponent<Status>().abilityCoolDown += effect;
            else affected.GetComponent<Status>().reserva += effect;
        }

        if (used && Input.GetKeyDown(KeyCode.Space) && !affected.GetComponent<Move>().isMoving && !FindAnyObjectByType<interfazBoton>().isInPause)
            Destroy(gameObject);
    }
}
