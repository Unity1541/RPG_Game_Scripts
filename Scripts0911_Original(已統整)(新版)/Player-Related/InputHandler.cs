using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float movementAmount;
    public PlayerInput playerInput;
    public Vector2 movementInput;
    public bool b_input;//預設值=false
    public bool rollFlag;
    public bool g_input;//給衝刺用
    public bool springFlag;
    public void OnEnable()//類似Awake一開始實作
    {
        if (playerInput ==null)
        {
           playerInput = new PlayerInput();
           playerInput.PlayerMovement.Movement.performed+=playerInput=> movementInput=playerInput.ReadValue<Vector2>();
           
        }
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    public void TickInput(float delta)
    {
        MoveInput(delta);
        HandleRollMovement(delta);
        HandleSprintMovement(delta);
    }
    public void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));
    }

    private void HandleRollMovement(float delta)
    {
        b_input=  playerInput.PlayerAction.Roll.triggered;
        if (b_input)
        {
            rollFlag=true;
        }
    }

    private void HandleSprintMovement(float delta)
    {
        g_input= playerInput.PlayerAction.Sprint.triggered;
        if(g_input)
        {
            springFlag=true;
        }
    }
}
