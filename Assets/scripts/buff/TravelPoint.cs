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
        turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && Input.GetKeyDown(KeyCode.F)
         && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            active = true;
            aux = false;
            affected = turn.currentPT;
            affected.GetComponent<Move>().enabled = false;
            affected.GetComponent<Status>().selectionMode = true;


        }
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.D) && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[3];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.A) && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[0];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.S) && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[1];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.W) && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[2];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.F) && aux && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                active = false;
                affected.GetComponent<Move>().enabled = true;
                affected.GetComponent<Status>().selectionMode = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !FindAnyObjectByType<interfazBoton>().isInPause) active = false;

        }

    }

}
