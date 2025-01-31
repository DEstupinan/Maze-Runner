using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    private TurnManager turnManager;
    private bool used=false;
    public string winnerN="";
    void Start()
    {
        turnManager = FindAnyObjectByType<TurnManager>();
    }
    void Update()
    {
        if (!used &&turnManager.currentPT.transform.position == transform.position && !turnManager.currentPT.GetComponent<Move>().isMoving
       && !FindAnyObjectByType<UIMain>().isInPause)
        {   
            used=true;
            winnerN=turnManager.currentPT.tag;
           FindAnyObjectByType<UIMain>().End();
           
        }
    }
}
