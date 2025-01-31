using UnityEngine;
using UnityEngine.Rendering.Universal;

public class trapVision : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private bool activeEffect = false;
    [SerializeField] private int duration = 3;
    private int count;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.GetComponent<AbilityWarlock>() == null && turn.currentPT.transform.position == transform.position
        && !turn.currentPT.GetComponent<Move>().isMoving && !activeEffect)
        {
            activeEffect = true;
            affected = turn.currentPT;
            GetComponent<SpriteRenderer>().enabled = true;
            affected.GetComponent<Status>().blind = true;

            count = affected.GetComponent<Status>().turnCount;
        }

        if (activeEffect)
        {


            if (!affected.GetComponent<Status>().blind)
            {
                Destroy(gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !affected.GetComponent<Move>().isMoving && !FindAnyObjectByType<UIMain>().isInPause)
            {

                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (count + duration == affected.GetComponent<Status>().turnCount)
            {

                affected.GetComponent<Status>().blind = false;
                affected.GetComponent<Light2D>().pointLightOuterRadius = affected.GetComponent<Status>().initialVision;
                Destroy(gameObject);
            }
        }

    }

}
