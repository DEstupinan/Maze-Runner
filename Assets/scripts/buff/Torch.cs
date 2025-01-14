using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private GameObject affected;
   [SerializeField] private float effect=3f;
    private bool active = false;
    private TurnManager turn;
    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.transform.position == transform.position)
        {
            affected = turn.currentPT;
            active = true;
        }
        if (active)
        {
            if (affected.GetComponent<Light2D>().pointLightOuterRadius == affected.GetComponent<Status>().initialVision)
            {
                affected.GetComponent<Status>().initialVision +=effect;
                affected.GetComponent<Light2D>().pointLightOuterRadius = affected.GetComponent<Status>().initialVision  ;
            }
            else affected.GetComponent<Status>().initialVision +=effect;
            Destroy(gameObject);
        }
    }
}
