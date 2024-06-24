using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "inputController/PlayerController")]
public class PlayerController : ScriptableObject
{
    public float JumpForce;
    public float MoveSpeed;

    public bool ReturnJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public float ReturnMovementInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
