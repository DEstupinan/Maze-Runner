using System;

using UnityEngine;
using UnityEngine.Rendering.Universal;



public class TurnManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraF;
    public GameObject currentPT;



    private int currentPlayerIndex = 0;

    void Start()
    {

        StartTurn();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !currentPT.GetComponent<Move>().isMoving)
        {
            EndTurn();
        }
    }


    private void StartTurn()
    {

        currentPT = GameObject.FindGameObjectWithTag($"Player{currentPlayerIndex + 1}");
        currentPT.GetComponent<Status>().turnCount++;
        if (currentPT.GetComponent<Status>().abilityCoolDown >0)
        {
            currentPT.GetComponent<Status>().abilityCoolDown--;
        }

        currentPT.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerCT";
        cameraF.target = currentPT;
        currentPT.GetComponent<Move>().enabled = true;
        currentPT.GetComponent<Light2D>().enabled = true;
        currentPT.GetComponent<Move>().ResetMoves();

    }


    private void EndTurn()
    {

        currentPT.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        currentPT.GetComponent<Move>().enabled = false;
        currentPT.GetComponent<Light2D>().enabled = false;
        currentPlayerIndex = (currentPlayerIndex + 1) % GameManager.Instance.playerCount;

        StartTurn();
    }

}
