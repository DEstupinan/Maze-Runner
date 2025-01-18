using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using URandom = UnityEngine.Random;
using Unity.Collections;

public class MazeLogic : MonoBehaviour
{
    public int row = 29, col = 29;
    private int centerX;
    private int centerY;
    private Vector2Int bestPosition;
    public int blockRange = 9;
    public GameObject wallPrefab, treasurePrefab, roadPrefab, TravelPointPrefab;
    public List<Vector3> TravelPointList;
    public List<GameObject> trapsPrefab, inevitableTrapPrefabs;
    public List<GameObject> Roads;
    public List<GameObject> Walls;
    public List<int> traps = new List<int> { 2, 2, 2 };
    public List<GameObject> buffPrefab;

    public List<int> buff = new List<int>();

    public int[,] maze;
    public GameObject[,] mazeObject;
    public List<Transform> players;
    private GameManager gameManager;

    private List<Vector2Int> directions = new List<Vector2Int>
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };

    void Start()
    {
        gameManager = GameManager.Instance;
        players = new List<Transform>();

        for (int x = 0; x < gameManager.playerCount; x++)
        {
            players.Add(GameObject.FindGameObjectWithTag($"Player{x + 1}").transform);
        }

        centerX = row / 2;
        centerY = col / 2;
        GenerateMaze();
        DrawMaze();
        PlaceTreasure();
        PlaceTravelPoint();
        PlaceTrap();
        PlaceBuff();

    }

    void GenerateMaze()
    {
        maze = new int[row, col];
        mazeObject = new GameObject[row, col];
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
                maze[i, j] = 1;

        int startX = URandom.Range(1, (row - 1) / 2) * 2 + 1;
        int startY = URandom.Range(1, (col - 1) / 2) * 2 + 1;

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
                {
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[0], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[1], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[2], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[3], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[4], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[5], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[6], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[7], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[8], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[9], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[10], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[11], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[12], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[13], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (Check(x, y) && maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Walls[14], new Vector3(x, y, 0), Quaternion.identity, transform);

                }
                if (!Check(x, y))
                {
                    if (x == 0 && y != 0 && y != row - 1)
                    {
                        if (maze[x + 1, y] == 0) mazeObject[x, y] = Instantiate(Walls[1], new Vector3(x, y, 0), Quaternion.identity, transform);
                        else mazeObject[x, y] = Instantiate(Walls[13], new Vector3(x, y, 0), Quaternion.identity, transform);
                    }
                    if (x == col - 1 && y != 0 && y != row - 1)
                    {
                        if (maze[x - 1, y] == 0) mazeObject[x, y] = Instantiate(Walls[1], new Vector3(x, y, 0), Quaternion.identity, transform);
                        else mazeObject[x, y] = Instantiate(Walls[14], new Vector3(x, y, 0), Quaternion.identity, transform);
                    }
                    if (y == 0 && x != 0 && x != col - 1)
                    {
                        if (maze[x, y + 1] == 0) mazeObject[x, y] = Instantiate(Walls[0], new Vector3(x, y, 0), Quaternion.identity, transform);
                        else mazeObject[x, y] = Instantiate(Walls[11], new Vector3(x, y, 0), Quaternion.identity, transform);
                    }
                    if (y == row - 1 && x != 0 && x != col - 1)
                    {
                        if (maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Walls[0], new Vector3(x, y, 0), Quaternion.identity, transform);
                        else mazeObject[x, y] = Instantiate(Walls[12], new Vector3(x, y, 0), Quaternion.identity, transform);
                    }

                }


                if (maze[x, y] == 0)
                {
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[0], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[1], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[2], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[3], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[4], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[5], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[6], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[7], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[8], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[9], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 1) mazeObject[x, y] = Instantiate(Roads[10], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 1 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[11], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 0 && maze[x - 1, y] == 0 && maze[x, y + 1] == 1 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[12], new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (maze[x + 1, y] == 1 && maze[x - 1, y] == 0 && maze[x, y + 1] == 0 && maze[x, y - 1] == 0) mazeObject[x, y] = Instantiate(Roads[13], new Vector3(x, y, 0), Quaternion.identity, transform);
                    
                }


            }
        mazeObject[0, 0] = Instantiate(Walls[5], new Vector3(0, 0, 0), Quaternion.identity, transform);
        mazeObject[0, row - 1] = Instantiate(Walls[3], new Vector3(0, row - 1, 0), Quaternion.identity, transform);
        mazeObject[col - 1, 0] = Instantiate(Walls[6], new Vector3(col - 1, 0, 0), Quaternion.identity, transform);
        mazeObject[row - 1, col - 1] = Instantiate(Walls[4], new Vector3(row - 1, col - 1, 0), Quaternion.identity, transform);
        for (int x = 0; x < row; x++)
            for (int y = 0; y < col; y++)
            {
                if (x == 0 || y == 0 || x == col-1 || y == row-1) maze[x, y] = -1;
            }
        bool Check(int x, int y)
        {
            return x - 1 >= 0 && x + 1 < col && y - 1 >= 0 && y + 1 < row;
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
            int startX = (int)i.position.x;
            int startY = (int)i.position.y;
            distances.Add(BFS(startX, startY));
        }
        bestPosition = FindBestCell(distances);
        maze[bestPosition.x, bestPosition.y] = 2;
        Instantiate(treasurePrefab, new Vector3(bestPosition.x, bestPosition.y, 0), Quaternion.identity, transform);
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



            foreach (Vector2Int dir in directions)
            {
                int nextX = current.x + dir.x;
                int nextY = current.y + dir.y;

                if (nextX >= 1 && nextX <= row - 2 && nextY >= 1 && nextY <= col - 2 && maze[nextX, nextY] != 1 && !visited[nextX, nextY])
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

        for (int x = centerX - blockRange / 2; x <= centerX + blockRange / 2; x++)
        {
            for (int y = centerY - blockRange / 2; y <= centerY + blockRange / 2; y++)
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

    void PlaceTrap()
    {
        List<Vector2Int> blocksCenter = new List<Vector2Int>
        {
            new Vector2Int(centerX - blockRange, centerY),
            new Vector2Int(centerX, centerY - blockRange),
            new Vector2Int(centerX, centerY),
            new Vector2Int(centerX, centerY + blockRange),
            new Vector2Int(centerX + blockRange, centerY)
        };
        Shuffle(blocksCenter);
        while (traps.Count() > 0)
        {
            foreach (Vector2Int block in blocksCenter)
            {
                while (true)
                {
                    int x = URandom.Range(block.x - blockRange / 2, 1 + block.x + blockRange / 2);
                    int y = URandom.Range(block.y - blockRange / 2, 1 + block.y + blockRange / 2);

                    if (maze[x, y] == 0)
                    {
                        int z = URandom.Range(0, traps.Count());
                        Instantiate(trapsPrefab[z], new Vector3(x, y, 0), Quaternion.identity, transform);
                        maze[x, y] = 3;
                        traps[z]--;
                        if (traps[z] == 0)
                        {
                            traps.RemoveAt(z);
                            trapsPrefab.RemoveAt(z);
                        }
                        break;
                    }
                }
                if (traps.Count() == 0) break;
            }
        }
        PlaceInevitableTrap();
    }
    void PlaceInevitableTrap()
    {
        if (inevitableTrapPrefabs.Count() == 0) return;
        int[,] playerDistances = BFS(bestPosition.x, bestPosition.y);


        Vector2Int[] array ={new Vector2Int(1,1),
                                 new Vector2Int(row-2,col-2),
                                 new Vector2Int(1,col-2),
                                 new Vector2Int(row-2,1)
                                };
        List<Vector2Int> shortRoad = new List<Vector2Int>();
        for (int i = 0; i < gameManager.playerCount; i++)
        {
            shortRoad = FindShortTrip(bestPosition, array[i], playerDistances);
            while (true)
            {
                int j = URandom.Range(1, shortRoad.Count() / 2);
                if (maze[shortRoad[j].x, shortRoad[j].y] == 0)
                {
                    int z = URandom.Range(0, inevitableTrapPrefabs.Count());
                    Instantiate(inevitableTrapPrefabs[z], new Vector3(shortRoad[j].x, shortRoad[j].y, 0), Quaternion.identity, transform);
                    maze[shortRoad[j].x, shortRoad[j].y] = 3;
                    break;
                }
            }
        }


    }
    List<Vector2Int> FindShortTrip(Vector2Int start, Vector2Int end, int[,] d)
    {
        List<Vector2Int> shortTrip = new List<Vector2Int>();
        shortTrip.Add(end);

        while (true)
        {


            foreach (Vector2Int dir in directions)
            {
                int nextX = end.x + dir.x;
                int nextY = end.y + dir.y;

                if (nextX >= 1 && nextX <= row - 2 && nextY >= 1 && nextY <= col - 2 && maze[nextX, nextY] != 1)
                {
                    if (d[nextX, nextY] < d[end.x, end.y])
                    {
                        shortTrip.Add(new Vector2Int(nextX, nextY));
                        end = new Vector2Int(nextX, nextY);
                    }
                }
            }
            if (end == start)
            {
                break;
            }
        }

        shortTrip.Reverse();
        return shortTrip;
    }
    void PlaceBuff()
    {
        List<Vector2Int> blocksCenter = new List<Vector2Int>
        {
            new Vector2Int(centerX - blockRange, centerY),
            new Vector2Int(centerX, centerY - blockRange),
            new Vector2Int(centerX, centerY + blockRange),
            new Vector2Int(centerX + blockRange, centerY)
        };
        Shuffle(blocksCenter);
        while (buff.Count() > 0)
        {
            foreach (Vector2Int block in blocksCenter)
            {
                while (true)
                {
                    int x = URandom.Range(block.x - blockRange / 2, 1 + block.x + blockRange / 2);
                    int y = URandom.Range(block.y - blockRange / 2, 1 + block.y + blockRange / 2);

                    if (maze[x, y] == 0)
                    {
                        int z = URandom.Range(0, buff.Count());
                        Instantiate(buffPrefab[z], new Vector3(x, y, 0), Quaternion.identity, transform);
                        maze[x, y] = 4;
                        buff[z]--;
                        if (buff[z] == 0)
                        {
                            buff.RemoveAt(z);
                            buffPrefab.RemoveAt(z);
                        }
                        break;
                    }
                }
                if (buff.Count() == 0) break;
            }
        }
    }
    void PlaceTravelPoint()
    {
        List<Vector2Int> blocksCenter = new List<Vector2Int>
        {
            new Vector2Int(2, centerY),
            new Vector2Int(centerX, 2),
            new Vector2Int(centerX, col-3),
            new Vector2Int(row-3, centerY)
        };

        foreach (Vector2Int block in blocksCenter)
        {
            while (true)
            {
                int x = URandom.Range(block.x - 1, block.x + 2);
                int y = URandom.Range(block.y - 1, block.y + 2);

                if (maze[x, y] == 0)
                {
                    Instantiate(TravelPointPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                    TravelPointList.Add(new Vector3(x, y));
                    maze[x, y] = 5;
                    break;
                }
            }
        }
    }
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {

            int randomIndex = URandom.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);

        }
    }
}

