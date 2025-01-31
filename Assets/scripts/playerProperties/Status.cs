
using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Status : MonoBehaviour
{

    [HideInInspector] public bool paralysis = false;
    [HideInInspector] public int count = 0;
    [HideInInspector] private bool used = false;
    [HideInInspector] public bool bomb = false;
    [HideInInspector] public bool torch = false;
    [HideInInspector] public bool selectionMode = false;
    [HideInInspector] public bool abilityActive = false;
    [HideInInspector] public int slot = 0;

    [HideInInspector] public bool refresh = false;
    [HideInInspector] public bool buff = false;
    [HideInInspector] public bool blind = false;
    public float initialVision = 3.5f;

    public int abilityCoolDown = 0;
    [HideInInspector] public int turnCount = 0;
    private TurnManager turn;

    void Start()
    {
        turn = FindAnyObjectByType<TurnManager>();
    }
    void Update()
    {
        if (paralysis)
        {
            GetComponent<Move>().moveAvailable = 0;
        }
        if (blind)
        {
            GetComponent<Light2D>().pointLightOuterRadius = 1.5f;
        }
        if (buff)
        {
            if (refresh && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause && gameObject == turn.currentPT)
            {
                abilityCoolDown = 0;
                refresh = false;
                buff = false;
            }
            if (bomb && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<UIMain>().isInPause && gameObject == turn.currentPT)
            {

                bomb = false;
                buff = false;
                MazeLogic mazeLogic = FindAnyObjectByType<MazeLogic>();
                List<Vector2Int> directions = new List<Vector2Int>
            {
                new Vector2Int(-1, -1),
                new Vector2Int(1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };
                foreach (Vector2Int dir in directions)
                {
                    if (mazeLogic.maze[(int)transform.position.x + dir.x, (int)transform.position.y + dir.y] == 1)
                    {
                        mazeLogic.maze[(int)transform.position.x + dir.x, (int)transform.position.y + dir.y] = 0;
                        Destroy(mazeLogic.mazeObject[(int)transform.position.x + dir.x, (int)transform.position.y + dir.y]);
                        mazeLogic.mazeObject[(int)transform.position.x + dir.x, (int)transform.position.y + dir.y] =
                        Instantiate(mazeLogic.bombRoadPrefab, new Vector3((int)transform.position.x + dir.x, (int)transform.position.y + dir.y), Quaternion.identity, mazeLogic.transform);
                    }

                }
            }
            if (torch)
            {
                if (!used && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && GetComponent<Light2D>().pointLightOuterRadius < 6f && !FindAnyObjectByType<UIMain>().isInPause && gameObject == turn.currentPT)
                {


                    GetComponent<Light2D>().pointLightOuterRadius = 6f;
                    used = true;
                    if (blind) blind = false;
                    count = turnCount;

                    if (blind)
                    {
                        torch = false;
                        buff = false;
                        used = false;

                    }

                }
                if (used && count + 3 == turnCount)
                {
                    GetComponent<Light2D>().pointLightOuterRadius = initialVision;

                    torch = false;
                    buff = false;
                    used = false;

                }
            }

        }



    }
}
