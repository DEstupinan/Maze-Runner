
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Status : MonoBehaviour
{

    public bool paralysis = false;
    public bool bomb = false;
    public bool torch = false;
    public bool selectionMode = false;
    public bool abilityActive = false;
    public int reserva = 0;

    public bool refresh = false;
    public bool buff = false;
    public bool blind = false;
    public float initialVision = 3.5f;

    public int abilityCoolDown = 0;
    public int turnCount = 0;

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
            if (refresh && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause)
            {
                abilityCoolDown = 0;
                refresh = false;
                buff = false;
            }
            if (bomb && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && !FindAnyObjectByType<interfazBoton>().isInPause)
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
                        Instantiate(mazeLogic.roadPrefab, new Vector3((int)transform.position.x + dir.x, (int)transform.position.y + dir.y), Quaternion.identity, mazeLogic.transform);
                    }

                }
            }
            if (torch && Input.GetKeyDown(KeyCode.R) && !GetComponent<Status>().selectionMode && GetComponent<Light2D>().pointLightOuterRadius < 6f && !FindAnyObjectByType<interfazBoton>().isInPause)
            {


                GetComponent<Light2D>().pointLightOuterRadius = 6f;

                if (blind) blind = false;
                int count = turnCount;

                if (blind)
                {
                    torch = false;
                    buff = false;

                }
                else if (count + 3 == turnCount)
                {
                    GetComponent<Light2D>().pointLightOuterRadius = initialVision;

                    torch = false;
                    buff = false;

                }
            }
        }



    }
}
