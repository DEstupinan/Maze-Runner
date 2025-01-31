using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{

    private TurnManager turn;
    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }

    void Update()
    {
        if (turn.currentPT.transform.position == transform.position && !turn.currentPT.GetComponent<Status>().buff
        && Input.GetKeyDown(KeyCode.F) && !FindAnyObjectByType<UIMain>().isInPause)
        {
            turn.currentPT.GetComponent<Status>().torch = true;
            turn.currentPT.GetComponent<Status>().buff = true;
            Destroy(gameObject);

        }

    }
}
