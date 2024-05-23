using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "inputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool ReturnJumpInput()
    {
        return Input.GetButtonDown("Space");
    }

    public override float ReturnMovementInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
