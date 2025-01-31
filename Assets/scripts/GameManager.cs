using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Players> characters;
    [HideInInspector] public int playerCount = 2;
    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
