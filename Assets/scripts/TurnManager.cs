using UnityEngine;
using UnityEngine.Rendering.Universal;


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

        if (Input.GetKeyDown(KeyCode.Space) && !currentPT.GetComponent<Move>().isMoving )
        {
            EndTurn();
        }
    }


    private void StartTurn()
    {
        currentPT = GameObject.FindGameObjectWithTag($"Player{currentPlayerIndex + 1}");
        currentPT.GetComponent<SpriteRenderer>().sortingOrder = 3;
        cameraF.target = currentPT;
        currentPT.GetComponent<Move>().enabled = true;
        currentPT.GetComponent<Light2D>().enabled = true;
        currentPT.GetComponent<Move>().ResetMoves();
    }


    private void EndTurn()
    {
        currentPT.GetComponent<SpriteRenderer>().sortingOrder = 2;
        currentPT.GetComponent<Move>().enabled = false;
        currentPT.GetComponent<Light2D>().enabled = false;
        currentPlayerIndex = (currentPlayerIndex + 1) % GameManager.Instance.playerCount;
        StartTurn();
    }
}
