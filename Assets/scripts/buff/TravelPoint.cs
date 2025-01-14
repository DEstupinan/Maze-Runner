using UnityEngine;

public class TravelPoint : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private MazeLogic mazeLogic;
    private bool aux;
    public bool active = false;



    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        mazeLogic = FindAnyObjectByType<MazeLogic>();
    }

    void Update()
    {
        aux = true;
        if (!active && (turn.currentPT.GetComponent<AbilityMage>() == null || !turn.currentPT.GetComponent<AbilityMage>().active) &&
        turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && Input.GetKeyDown(KeyCode.T))
        {
            active = true;
            aux = false;
            affected = turn.currentPT;
            affected.GetComponent<Move>().enabled = false;


        }
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                affected.transform.position = mazeLogic.hogueraList[3];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                affected.transform.position = mazeLogic.hogueraList[0];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                affected.transform.position = mazeLogic.hogueraList[1];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                affected.transform.position = mazeLogic.hogueraList[2];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.T) && aux)
            {
                active = false;
                affected.GetComponent<Move>().enabled = true;
            }

        }

    }

}
