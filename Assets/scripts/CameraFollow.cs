using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public GameObject target;

    public float speed;
    [HideInInspector] public bool active;


    void Start()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player1").transform.position.x, GameObject.FindGameObjectWithTag("Player1").transform.position.y, -10);
    }

    void MoveCamera()
    {   
        //logic for the camera to follow the player who has the turn active taking reference from the turnmanager
        if (active && target)
        {
            Vector3 targetPosition = target.transform.position;

            transform.position = Vector3.Lerp(transform.position,
            new Vector3(targetPosition.x, targetPosition.y, -10), speed * Time.deltaTime);
        }

    }
    void Update()
    {

        MoveCamera();
    }
}