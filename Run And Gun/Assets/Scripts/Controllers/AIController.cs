using UnityEngine;



[CreateAssetMenu(fileName = "AIController", menuName = "inputController/AIController")]

public class AIController : InputController
{
    public override bool ReturnJumpInput()
    {
        return true;
    }

    public override float ReturnMovementInput()
    {
        return 1f;
    }
}
