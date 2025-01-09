
using UnityEngine;
using UnityEngine.SceneManagement;
public class Move : MonoBehaviour
{
    private float speed = 2;
    private int moveAvailable;
    [SerializeField] private int moveInitial;
    private Vector2 targetPosition;
    private MazeLogic maze;
    public bool isMoving = false;
    Vector2 input;
    Vector2 lastInput;


    void Start()
    {
        targetPosition = transform.position;
        lastInput = Vector2.zero;
        moveAvailable = moveInitial;
        maze = FindAnyObjectByType<MazeLogic>();
    }


    void Update()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != lastInput)
        {
            lastInput = input;


            if ((input.x != 0 ^ input.y != 0) && !isMoving)
            {
                
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                int x_ = (int)input.x;
                int y_ = (int)input.y;
                if (maze.GetValue(x + x_, y + y_) != 1)
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
        if(maze.GetValue((int)transform.position.x,(int)transform.position.y)==2 && !isMoving)
        {
             SceneManager.LoadScene("final");
        }
        if (moveAvailable == 0 && !isMoving)
        {
            this.enabled = false;

        }

    }
    public void ResetMoves()
    {
        moveAvailable = moveInitial;
        lastInput = Vector2.zero;
        targetPosition = transform.position;
    }


}
