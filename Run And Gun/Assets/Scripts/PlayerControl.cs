using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerController Controller;
    Rigidbody2D rb;
    bool IsGrounded = false;
    float JumpBuffer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        JumpBuffer -= Time.deltaTime;

        rb.velocity = new Vector2(Controller.ReturnMovementInput() * Controller.MoveSpeed, rb.velocity.y);
        if (Controller.ReturnJumpInput() && IsGrounded)
        {
            Jump();
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
        if (Controller.ReturnJumpInput() && !IsGrounded)
        {
            JumpBuffer = 0.3f;
        }
        if (JumpBuffer > 0 && IsGrounded)
        {
            Jump();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            IsGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    /// <summary>
    /// Springt en stopt RememberJumpInput coroutine
    /// </summary>
    void Jump()
    {
        JumpBuffer = -1;
        //wordt gestopt zodat je jump niet meer aangeroepen wordt na het springen
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }
}
