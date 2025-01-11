using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Status : MonoBehaviour
{
    public bool paralysis = false;
    public bool blind = false;
    public int turnCount = 0;
    void Update()
    {
        if (paralysis)
        {
            this.GetComponent<Move>().enabled = false;
        }
        if(blind)
        {
            this.GetComponent<Light2D>().pointLightOuterRadius= 1.5f;
        }
    }
}
