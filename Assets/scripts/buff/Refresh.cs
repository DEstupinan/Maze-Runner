using UnityEngine;

public class Refresh : MonoBehaviour
{
   
    
    private TurnManager turn;
    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Status>().buff)
        {
            
            turn.currentPT.GetComponent<Status>().refresh=true;
            turn.currentPT.GetComponent<Status>().buff=true;
            Destroy(gameObject);   
        }
        
    }
}
