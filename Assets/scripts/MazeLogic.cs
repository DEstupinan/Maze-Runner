using UnityEngine;
using System.Collections.Generic;

using System.Linq;
using UnityEngine.Rendering;
using Unity.Collections;

public class MazeLogic : MonoBehaviour
{
    public int row, col;
    public int centerRange;
    public GameObject wallPrefab, treasurePrefab, roadPrefab;
    public float wallSize = 1f;
    public int[,] maze;
    public List<Transform> players;
    private GameManager gameManager;
    private GameObject[] playerObjects;

    void Start()
    {
        gameManager = GameManager.Instance;
        players = new List<Transform>();
        GameObject[] playerObjects = new GameObject[gameManager.playerCount];
        for (int x = 0; x < gameManager.playerCount; x++)
        {
            playerObjects[x] = GameObject.FindGameObjectWithTag($"Player{x + 1}");


        }


        foreach (GameObject i in playerObjects)
        {
            players.Add(i.transform);
        }
        GenerateMaze();
        DrawMaze();
        PlaceTreasure();
    }

    void GenerateMaze()
    {
        maze = new int[row, col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                maze[i, j] = 1;
            }
        }

        int randomX = Random.Range(1, (row - 2) / 2);
        int randomY = Random.Range(1, (col - 1) / 2);

        int startX;
        int startY;

        if (randomX == 1) startX = randomX;
        else startX = randomX * 2 + 1;

        if (randomY == 1) startY = randomY;
        else startY = randomY * 2 + 1;

        CarvePath(startX, startY);

    }

    void CarvePath(int x, int y)
    {
        maze[x, y] = 0;


        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 2),
            new Vector2Int(0, -2),
            new Vector2Int(2, 0),
            new Vector2Int(-2, 0)
        };
        Shuffle(directions);

        foreach (Vector2Int dir in directions)
        {
            int nextX = x + dir.x;
            int nextY = y + dir.y;

            if (nextX > 0 && nextX < row && nextY > 0 && nextY < col && maze[nextX, nextY] == 1)
            {
                maze[x + dir.x / 2, y + dir.y / 2] = 0;
                CarvePath(nextX, nextY);
            }
        }
    }

    void DrawMaze()
    {
        for (int x = 0; x < row; x++)
            for (int y = 0; y < col; y++)
            {
                if (maze[x, y] == 1)
                    Instantiate(wallPrefab, new Vector3(x * wallSize, y * wallSize, 0), Quaternion.identity, transform);
                if (maze[x, y] == 0)
                    Instantiate(roadPrefab, new Vector3(x * wallSize, y * wallSize, 0), Quaternion.identity, transform);
            }
    }

    public int GetValue(int i, int j)
    {
        return maze[i, j];
    }

    void PlaceTreasure()
    {
        List<int[,]> distances = new List<int[,]>();
        foreach (Transform i in players)
        {
            int startX = Mathf.RoundToInt(i.position.x / wallSize);
            int startY = Mathf.RoundToInt(i.position.y / wallSize);
            distances.Add(BFS(startX, startY));
        }
        Vector2Int bestPosition = FindBestCell(distances);
        Instantiate(treasurePrefab, new Vector3(bestPosition.x * wallSize, bestPosition.y * wallSize, 0), Quaternion.identity, transform);
    }

    int[,] BFS(int startX, int startY)
    {
        int[,] distance = new int[row, col];
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        bool[,] visited = new bool[row, col];

        queue.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;
        distance[startX, startY] = 0;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            int currentDistance = distance[current.x, current.y];

            List<Vector2Int> directions = new List<Vector2Int>
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };

            foreach (Vector2Int dir in directions)
            {
                int nextX = current.x + dir.x;
                int nextY = current.y + dir.y;

                if (nextX >= 0 && nextX < row && nextY >= 0 && nextY < col && maze[nextX, nextY] == 0 && !visited[nextX, nextY])
                {
                    visited[nextX, nextY] = true;
                    distance[nextX, nextY] = currentDistance + 1;
                    queue.Enqueue(new Vector2Int(nextX, nextY));
                }
            }
        }

        return distance;
    }

    Vector2Int FindBestCell(List<int[,]> distances)
    {
        Vector2Int bestPosition = new Vector2Int(-1, -1);
        float minDifference = float.MaxValue;
        float minDistanceToCenter = float.MaxValue;



        int centerX = row / 2;
        int centerY = col / 2;

        for (int x = centerX - centerRange / 2; x <= centerX + centerRange / 2; x++)
        {
            for (int y = centerY - centerRange / 2; y <= centerY + centerRange / 2; y++)
            {
                if (x >= 0 && x < row && y >= 0 && y < col && maze[x, y] == 0)
                {
                    int maxDistance = distances.Max(d => d[x, y]);
                    int minDistance = distances.Min(d => d[x, y]);
                    float difference = maxDistance - minDistance;

                    float distanceToCenter = Vector2Int.Distance(new Vector2Int(x, y), new Vector2Int(centerX, centerY));

                    if (difference < minDifference || (difference == minDifference && distanceToCenter < minDistanceToCenter))
                    {
                        minDifference = difference;
                        minDistanceToCenter = distanceToCenter;
                        bestPosition = new Vector2Int(x, y);
                    }
                }
            }
        }

        return bestPosition;
    }
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {

            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);

        }
    }
}

