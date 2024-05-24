using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "inputController/PlayerController")]
public class PlayerController : InputController
{
    public float JumpForce;
    public float MoveSpeed;

    public override bool ReturnJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float ReturnMovementInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
