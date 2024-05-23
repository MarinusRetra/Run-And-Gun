using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool ReturnJumpInput();
    public abstract float ReturnMovementInput();
}
