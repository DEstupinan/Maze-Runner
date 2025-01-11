using UnityEngine;
using UnityEngine.Rendering.Universal;

public class trapVision : MonoBehaviour
{
    private TurnManager turn;
    private GameObject affected;
    private bool used = false;
    [SerializeField] private int effect = 3;
    private int count;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Move>().isMoving && !used)
        {
            used = true;
            affected = turn.currentPT;
            this.GetComponent<SpriteRenderer>().enabled = true;
            affected.GetComponent<Light2D>().pointLightOuterRadius = 0.5f;

            count = affected.GetComponent<Status>().turnCount;
        }
        if (used)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !affected.GetComponent<Move>().isMoving)
            {
                affected.GetComponent<Status>().blind = true;
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (count + effect + 1 == affected.GetComponent<Status>().turnCount)
            {
                affected.GetComponent<Status>().paralysis = false;
                affected.GetComponent<Status>().blind = false;
                affected.GetComponent<Light2D>().pointLightOuterRadius = 3.5f;
                Destroy(gameObject);
            }
        }

    }

}
