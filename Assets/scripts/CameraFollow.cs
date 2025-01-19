using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    public float speed;
    public bool active;


 void Start()
 {
    transform.position=GameObject.FindGameObjectWithTag("Player1").transform.position;
 }

    void MoveCamera()
    {
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