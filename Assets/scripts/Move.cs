
using UnityEngine;

public class Move : MonoBehaviour
{
     private float speed=2;
     private int moveAvailable; 
     [SerializeField] private int moveInitial;
     private Vector2 targetPosition;
    private MazeLogic maze;
    private bool isMoving = false;
    Vector2 input;
    Vector2 lastInput;


    void Start()
    {
        targetPosition = transform.position;
        lastInput=Vector2.zero;
        moveAvailable=moveInitial;
    }


    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(input!=lastInput)
        {
            lastInput = input;
           

            if ((input.x != 0 ^ input.y != 0) && !isMoving)
            {
                maze = FindAnyObjectByType<MazeLogic>();
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                int x_ = (int)input.x;
                int y_ = (int)input.y;
                if (maze.GetValue(x + x_, y + y_) == 0)
                {
                    isMoving = true;
                    targetPosition += input;
                    moveAvailable--;
                }
            }
        }
         if (isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetPosition) == 0)
                {
                isMoving = false;
                }
            }

        if(moveAvailable==0)
        {
            this.enabled=false;
        }

    }
     public void ResetMoves()
    {
        moveAvailable = moveInitial;
    }


}
