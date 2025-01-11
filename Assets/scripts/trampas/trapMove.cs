using UnityEngine;

public class trapMove : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private bool used = false;
    [SerializeField] private int effect = 2;
    private int count;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && !used)
        {
            used = true;
            affected = turn.currentPT;
            this.GetComponent<SpriteRenderer>().enabled = true;
            affected.GetComponent<Move>().enabled = false;
            affected.GetComponent<Status>().paralysis = true;
            count = affected.GetComponent<Status>().turnCount;
        }
        if (used)
        {
            if (count + effect + 1 == affected.GetComponent<Status>().turnCount)
            {
                affected.GetComponent<Status>().paralysis = false;
                affected.GetComponent<Move>().enabled = true;
                Destroy(gameObject);
            }
        }

    }

}
