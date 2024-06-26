using UnityEngine;
using System;
using System.Collections.Generic;


public class AIMovement : MonoBehaviour
{
    public AiController Controller;
    Rigidbody2D rb;
    bool SwapPoint;
    public GameObject[] MovePoints;
    public Transform[] JumpPoints;
    SpriteRenderer SpriteRenderer;
    KeyValuePair<float, bool> MovementInfo;

    private void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SwapPoint = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));// dit is zodat niet elke ai dezelfde kant op loopt.
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
           //Controller.ReturnMovementInput(MovePoints, transform.position, SwapPoint);

           //Zet de velocity op basis van waar de volgende movePoint is
           try
           {
               MovementInfo = Controller.ReturnMovementInput(MovePoints, transform.position, SwapPoint);
           }
           catch { }

           rb.velocity = new Vector2(MovementInfo.Key * Controller.MoveSpeed, rb.velocity.y);
           SwapPoint = MovementInfo.Value;


        //Als de AI op een JumpPoint staat en jumpPoints ingesteld heeft, spring
        try
        { 
            if (Controller.ReturnJumpInput(JumpPoints, transform.position))
            {
                Jump();
            }
        }
        catch { }
        // deze try catch is om een mogelijke null reference exception te vangen

        //Draait de ai op basis van velocity
        if (rb.velocity.x >= 0.01f)
        {
           SpriteRenderer.transform.localScale = new Vector3(-6, 6f, 6f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
           SpriteRenderer.transform.localScale = new Vector3(6f, 6f, 6f);
        }
    }

    void Jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, Controller.JumpForce);
    }

    void ToggleSwapPoint()
    { 
       SwapPoint = !SwapPoint;
    }

}
