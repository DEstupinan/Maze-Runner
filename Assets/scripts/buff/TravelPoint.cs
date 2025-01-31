using Unity.VisualScripting;
using UnityEngine;

public class TravelPoint : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private MazeLogic mazeLogic;
    private bool aux;
    [HideInInspector] public bool active = false;



    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        mazeLogic = FindAnyObjectByType<MazeLogic>();
    }

    void Update()
    {   //If the conditions are met, activate the portal and enter the selection mode
        aux = true;
        if (!active && !turn.currentPT.GetComponent<Status>().selectionMode &&
        turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && Input.GetKeyDown(KeyCode.F)
         && !FindAnyObjectByType<UIMain>().isInPause)
        {
            active = true;

            aux = false;
            affected = turn.currentPT;
            affected.GetComponent<Move>().enabled = false;
            affected.GetComponent<Status>().selectionMode = true;


        }
        if (active)
        {   
            //If the portal is active, the movement keys are now used to switch between portals
            if (Input.GetKeyDown(KeyCode.D) && !FindAnyObjectByType<UIMain>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[3];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.A) && !FindAnyObjectByType<UIMain>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[0];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.S) && !FindAnyObjectByType<UIMain>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[1];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.W) && !FindAnyObjectByType<UIMain>().isInPause)
            {
                affected.transform.position = mazeLogic.TravelPointList[2];
                affected.GetComponent<Move>().targetPosition = affected.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.F) && aux && !FindAnyObjectByType<UIMain>().isInPause)
            {
                active = false;
                affected.GetComponent<Move>().enabled = true;
                //It exits the portal with a small delay to prevent it from entering 
                //the new one since it receives the entry and exit key in the same frame
                Invoke("Disable", 0.01f);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !FindAnyObjectByType<UIMain>().isInPause)
            {
                active = false;
                affected.GetComponent<Status>().selectionMode = false;
            }

        }

    }
    void Disable()
    {
        affected.GetComponent<Status>().selectionMode = false;
    }

}
