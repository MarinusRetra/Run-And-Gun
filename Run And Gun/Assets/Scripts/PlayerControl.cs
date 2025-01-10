using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //public InputActionReference moveLookRef;
    //public InputActionReference jumpRef;
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    private bool IsGrounded = true;
    private float JumpBuffer = 0f;
    private readonly float JumpBufferTimer = 0.3f;


   // private void OnEnable()
   // {
   //     jumpRef.action.started += ReadJumpRef;
   // }
   //
   // private void OnDisable()
   // {
   //     jumpRef.action.started -= ReadJumpRef;
   // }


    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -13f);
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //link en rechts movement
      ///  rb.velocity = new Vector2(moveLookRef.action.ReadValue<Vector2>().x * moveSpeed, rb.velocity.y);

        //kan alleen springen als je op de grond staat

        TurnToCursor();
    }
    void Jump()
    {
        IsGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    /// <summary>
    /// Deze functie wordt aangeroepen elke keer wanneer jump input wordt gelezen
    /// </summary>
    /// <param name="obj"></param>
   // private void ReadJumpRef(InputAction.CallbackContext obj)
   // {
   //     JumpBuffer = JumpBufferTimer;
   //     
   //     //De jumpbuffer telt af als je op spatie drukt
   //     JumpBuffer -= Time.deltaTime;
   //     
   //     //Verlaagt de velocity als jump niet ingedrukt wordt en de y velocity hoger dan 0 is.
   //     // if (!pressed && (rb.velocity.y > 0f))
   //     //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
   //     
   //     //Springt als de jumpbuffer hoger is dan null en je op de grond staat
   //     ///Ik check niet voor input want jumpbuffer is altijd hoger dan 0 als je op spatie drukt
   //     if (JumpBuffer > 0 && IsGrounded)
   //         Jump();
   // }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!IsGrounded) //Is grounded hoeft niet op true gezet te worden als dat al zo is.
        { 
            if (collision.gameObject.CompareTag("Ground") && JumpBuffer > 0)
            {
                IsGrounded = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGrounded) //Is grounden kan niet twee keer false zijn dus ik hoef ook geen extra check te doen.
        { 
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsGrounded = false;
            }
        }
    }
}
