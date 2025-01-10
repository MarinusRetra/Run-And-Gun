using Awesome_Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]

public class InputReader : ScriptableObject, PlayerInputs.IGameplayActions, PlayerInputs.IUIActions
{//Deze class gebruikt de interface van GameplayActions en UIActionss zodat ik deze later kan overidden 

    PlayerInputs _playerInputs;

    private void OnEnable()
    {
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();

            //Cleared de list van huidige callbacks en roept de removecallback functie aan over alle verwijderde instances
            //zijn en stopt alle doorgegeven callbacks in de lijst en roept op elk toegevoegde element AddCallback aan.
            _playerInputs.Gameplay.SetCallbacks(instance:this);
            _playerInputs.UI.SetCallbacks(instance:this);

            SetGameplay();
        }
    }
    /// <summary>
    /// Zet Gameplay ActionMap en UI ActionMap uit
    /// </summary>
    void SetGameplay()
    {
        _playerInputs.Gameplay.Enable();
        _playerInputs.UI.Disable();
    }
    /// <summary>
    /// Zet UI ActionMap aan en Gameplay ActionMap uit
    /// </summary>
    void SetUi()
    {
        _playerInputs.Gameplay.Disable();
        _playerInputs.UI.Enable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCancelled;
    public event Action DashEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    /// De CallbackContext geeft toegang tot de phases van elke InputAction: started, performed en canceled.
    //Gameplay
    public void OnDash(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnMoveLook(InputAction.CallbackContext context)
    {
        Debug.Log(message:$"Phase: {context.phase}, Value: {context.ReadValue<Vector2>()}");
    }

    public void OnPause(InputAction.CallbackContext context)
    {

    }

    //UI
    public void OnResume(InputAction.CallbackContext context)
    {

    }
}
