using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public AiController Controller;
    Rigidbody2D rb;
    public GameObject[] MovePoints;
    public Transform[] JumpPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        //zet de velocity op basis van waar de volgende movePoint is
        rb.velocity = new Vector2(Controller.ReturnMovementInput(MovePoints, transform.position) * Controller.MoveSpeed, rb.velocity.y);

        //als de AI op een JumpPoint staat Jump()
        if (Controller.ReturnJumpInput(JumpPoints, transform.position))
        {
            Jump();
        }

        //Draait de ai op basis van zijn velocity
        if (rb.velocity.x >= 0.01f)
        {
           transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
           transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }

}
