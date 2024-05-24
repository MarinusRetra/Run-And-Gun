using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerController Controller;
    Rigidbody2D rb;
    bool IsGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
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
            StartCoroutine(RememberJumpInput());
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
        //wordt gestopt zodat je jump niet meer aangeroepen wordt na het springen
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }

    /// <summary>
    /// Zorgt ervoor dat je meteen springt wanneer je de grond raakt als je vlak ervoor op de spring knop drukte
    /// </summary>
    /// <returns></returns>
    IEnumerator RememberJumpInput()
    {
        // ik check 4 keer in een for loop inplaats van 1 check met een 0.2 seconden voor een iets soepelere sprong
        for (int i = 0; i < 4; i++)
        {
          yield return new WaitForSeconds(0.05f);
          if (IsGrounded)
          { 
            Jump(); 
            StopCoroutine(RememberJumpInput());
          }
        }
    }

}
