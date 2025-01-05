using UnityEngine;


public class TurnManager : MonoBehaviour
{
    [SerializeField] private CameraFollow camerafollow;
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
       camerafollow.target =currentPT ;
       currentPT.GetComponent<Movimiento>().enabled=!currentPT.GetComponent<Movimiento>().enabled;
    }

    
    private void EndTurn()
    {   
        currentPT.GetComponent<Movimiento>().enabled=!currentPT.GetComponent<Movimiento>().enabled;
        currentPlayerIndex = (currentPlayerIndex + 1) % GameManager.Instance.playerCount;
        StartTurn();
    }
}
