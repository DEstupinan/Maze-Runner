
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Status : MonoBehaviour
{
    public bool paralysis = false;
    public bool blind = false;
    public float initialVision=3.5f;
    public int abilityCoolDown=0;
    public int turnCount = 0;
    void Update()
    {
        if (paralysis)
        {
            GetComponent<Move>().moveAvailable=0;
        }
        if(blind)
        {
            GetComponent<Light2D>().pointLightOuterRadius= 1.5f;
        }
     
    }
}
