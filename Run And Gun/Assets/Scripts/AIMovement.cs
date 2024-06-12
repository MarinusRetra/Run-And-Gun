using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public AiController Controller;
    Rigidbody2D rb;
    public GameObject[] MovePoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        //zet de velocity op basis van waar de volgende movePoint is
        rb.velocity = new Vector2(Controller.ReturnMovementInput(MovePoints, gameObject, rb) * Controller.MoveSpeed, rb.velocity.y);

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

}
