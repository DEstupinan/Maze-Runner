using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;



public class TurnManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraF;
    public GameObject currentPT;
    public TMP_Text textCurrentPT;
    public TMP_Text textMoveAvaible;
    public TMP_Text textAbilityCoolDown;
    public TMP_Text textBuff;
    public TMP_Text textStatus;

    private string tagPT;


    private int currentPlayerIndex = 0;

    void Start()
    {

        StartTurn();
    }

    void Update()
    {
        Text();







        if (Input.GetKeyDown(KeyCode.Space) && !currentPT.GetComponent<Move>().isMoving && !FindAnyObjectByType<interfazBoton>().isInPause)
        {
            EndTurn();
        }
    }


    private void StartTurn()
    {

        currentPT = GameObject.FindGameObjectWithTag($"Player{currentPlayerIndex + 1}");
        tagPT = $"{currentPlayerIndex + 1}";
        textCurrentPT.text = tagPT;

        currentPT.GetComponent<Status>().turnCount++;
        if (currentPT.GetComponent<Status>().abilityCoolDown > 0)
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
    void Text()
    {
        textMoveAvaible.text = currentPT.GetComponent<Move>().moveAvailable.ToString();
        if (currentPT.GetComponent<Status>().abilityCoolDown == 0)
        {
            Color greenP;
            ColorUtility.TryParseHtmlString("#12EC2C", out greenP);
            textAbilityCoolDown.color = greenP;
            if (currentPT.GetComponent<Status>().abilityActive)

                textAbilityCoolDown.text = "Activa";


            else
                textAbilityCoolDown.text = "Disponible";

        }
        else
        {
            textAbilityCoolDown.text = currentPT.GetComponent<Status>().abilityCoolDown.ToString();
            Color redP;
            ColorUtility.TryParseHtmlString("#EC1112", out redP);
            textAbilityCoolDown.color = redP;
        }
        if (currentPT.GetComponent<Status>().buff)
        {
            if (currentPT.GetComponent<Status>().torch)
                textBuff.text = "Antorcha";
            if (currentPT.GetComponent<Status>().bomb) textBuff.text = "Bomba";
            if (currentPT.GetComponent<Status>().refresh) textBuff.text = "Gema";
        }
        else textBuff.text = "Ninguno";
        textStatus.text = "SinCambios";
        if (currentPT.GetComponent<Move>().isMoving) textStatus.text = "Moviendo";
        if (currentPT.GetComponent<Status>().blind) textStatus.text = "Cegado";
        if (currentPT.GetComponent<Status>().selectionMode) textStatus.text = "ModoSelección";
        if (currentPT.GetComponent<Status>().paralysis) textStatus.text = "Paralizado";
    }

}
