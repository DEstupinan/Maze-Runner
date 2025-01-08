using UnityEngine;


public class TurnManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraF;
    private GameObject currentPT;
    
    private int currentPlayerIndex = 0;

    void Start()
    {
        
        StartTurn();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    
    private void StartTurn()
    {  
        currentPT=GameObject.FindGameObjectWithTag($"Player{currentPlayerIndex+1}");
       cameraF.target =currentPT ;
       currentPT.GetComponent<Move>().enabled=true;
       currentPT.GetComponent<Move>().ResetMoves();
    }

    
    private void EndTurn()
    {   
        currentPT.GetComponent<Move>().enabled=false;
        currentPlayerIndex = (currentPlayerIndex + 1) % GameManager.Instance.playerCount;
        StartTurn();
    }
}
