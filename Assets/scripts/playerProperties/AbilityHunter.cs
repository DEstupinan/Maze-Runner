using JetBrains.Annotations;
using UnityEngine;

public class AbilityHunter : MonoBehaviour
{
    public int coolDown = 4;
    [SerializeField] private GameObject trapPrefab;
    private GameObject trap;
    private TurnManager turn;
    private bool used = false;
    private Move move;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
        move = GetComponent<Move>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !move.isMoving && GetComponent<Status>().abilityCoolDown == 0 && gameObject == turn.currentPT)
        {
            GetComponent<Status>().abilityCoolDown = coolDown;
            trap = Instantiate(trapPrefab, transform.position, Quaternion.identity);
            used = true;

        }
        if (used && Input.GetKeyDown(KeyCode.Space)) 
        {
            used=false;
            trap.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}