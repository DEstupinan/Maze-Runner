
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 puntoDestino;
    private MazeLogic maze;
    private bool its_in_mov = false;
    Vector2 input;
    Vector2 lastInput;


    void Start()
    {
        puntoDestino = transform.position;
        lastInput=Vector2.zero;
    }


    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(input!=lastInput)
        {
            lastInput = input;
           

            if ((input.x != 0 ^ input.y != 0) && !its_in_mov)
            {
                maze = FindAnyObjectByType<MazeLogic>();
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                int x_ = (int)input.x;
                int y_ = (int)input.y;
                if (maze.GetValue(x + x_, y + y_) == 0)
                {
                    its_in_mov = true;
                    puntoDestino += input;
                }
            }
        }
         if (its_in_mov)
            {
                transform.position = Vector2.MoveTowards(transform.position, puntoDestino, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, puntoDestino) == 0)
                {
                its_in_mov = false;
                }
            }

    }


}
