using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerController Controller;
    Rigidbody2D rb;
    bool IsGrounded = false;
    float JumpBufferTimer = 0.1f;
    float JumpBuffer = 0f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //link en rechts movement
        rb.velocity = new Vector2(Controller.ReturnMovementInput() * Controller.MoveSpeed, rb.velocity.y);
        

        //kan alleen springen als je op de grond staat
        if (JumpBuffer > 0 && IsGrounded)
        {
            Jump();
        }

        // verlaagt de velocity als je jump loslaat
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //jump buffer
        if (Controller.ReturnJumpInput())
        {
            JumpBuffer = JumpBufferTimer;
        }
        else
        { 
            JumpBuffer -= Time.deltaTime;
        }
        TurnToCursor();
    }
    void Jump()
    {
        IsGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }

    /// <summary>
    /// Zorgt dat de speler altijd de kant van de cursor op kijkt
    /// </summary>
    void TurnToCursor()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPos.x >= transform.position.x)
        {
           transform.localScale = new Vector3(1, 1f, 1f);
        }
        else if (mouseWorldPos.x <= transform.position.x)
        {
           transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            IsGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && JumpBuffer > 0)
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

}
