using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int row = 21;
    public int col = 21;
    public GameObject wallPrefab;
    public float wallSize = 1f;

    private int[,] maze;

    void Start()
    {
        GenerateMaze();
        DrawMaze();
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

        
        int startX = Random.Range(1, row-1);
        int startY = Random.Range(1, col-1);
        
        CarvePath(startX, startY);
    }

    void CarvePath(int x, int y)
    {
        maze[x, y] = 0; 

        
        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 2),  // Arriba
            new Vector2Int(0, -2), // Abajo
            new Vector2Int(2, 0),  // Derecha
            new Vector2Int(-2, 0)  // Izquierda
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
        {
            for (int y = 0; y < col; y++)
            {
                if (maze[x, y] == 1)
                {
                    Vector3 position = new Vector3(x * wallSize, y * wallSize, 0);
                    Instantiate(wallPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

