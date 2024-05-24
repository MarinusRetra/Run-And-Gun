using System.Collections;
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

    /// <summary>
    /// Springt en stopt RememberJumpInput coroutine
    /// </summary>
    void Jump()
    {
        IsGrounded = false;
        //wordt gestopt zodat je jump niet meer aangeroepen wordt na het springen
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }
}
