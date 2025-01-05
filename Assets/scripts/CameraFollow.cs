using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    private float target_posX;
    private float target_posY;
    private float posX;
    private float posY;
    public float left;
    public float right;
    public float up;
    public float down;
    public float speed;
    public bool active;

    void Start()
    {   
        target=GameObject.FindGameObjectWithTag("Player");
        posX = target_posX + left;
        posY = target_posY + down;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -10), 1);
    }

    void MoveCamera()
    {
        if (active)
        {
            if (target)
            {
                target_posX = target.transform.position.x;
                target_posY = target.transform.position.y;
                if (target_posX > left && target_posX < right)
                {
                    posX = target_posX; 
                }
                if (target_posY<up && target_posY>down)
                {
                    posY=target_posY;
                }
            }
        }
        transform.position = Vector3.Lerp(transform.position,new Vector3(posX, posY,-10),speed*Time.deltaTime);
    }
    void Update()
    {

       MoveCamera();
    }
}