using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AbilityOracle : MonoBehaviour
{
    public int coolDown = 5;
    public float power = 8f;
    public int duration = 3;
    private TurnManager turn;
    private Light2D lightV;
    private Move move;
    private int count;
    [HideInInspector] public bool active = false;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        lightV = GetComponent<Light2D>();
        move = GetComponent<Move>();
    }
    void Update()
    {
        //Requirements to be met to activate
        if (Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0
        && gameObject == turn.currentPT && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause)
        {

            lightV.pointLightOuterRadius = power;
            active = true;
            GetComponent<Status>().abilityActive = true;
            count = GetComponent<Status>().turnCount;
            if (GetComponent<Status>().blind)
            {
                GetComponent<Status>().blind = false;

            }

        }
        if (active)
        {   
            //Logic to disable the ability when its duration expires or when blinded
            if (GetComponent<Status>().blind)
            {
                active = false;
                GetComponent<Status>().abilityCoolDown = coolDown;
            }

            else if (count + duration == GetComponent<Status>().turnCount)
            {

                lightV.pointLightOuterRadius = GetComponent<Status>().initialVision;
                active = false;
                GetComponent<Status>().abilityActive = false;
                GetComponent<Status>().abilityCoolDown += coolDown + GetComponent<Status>().slot;
                GetComponent<Status>().slot = 0;
            }
        }


    }
}

