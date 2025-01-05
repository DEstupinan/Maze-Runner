using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    void Awake()
    {
        int indexList=PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(GameManager.Instance.characters[indexList].player,transform.position,Quaternion.identity);
    }

    
}
