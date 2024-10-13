using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    public float horizontal;
    public float vertical;
    public float movementAmount;
    public PlayerInput playerInput;
    public Vector2 movementInput;
    public bool b_input;//預設值=false
    public bool rollFlag;
    public bool comboFlag;
    public bool g_input;//給衝刺用
    public bool Re_input,Rb_input,Rf_input;//攻擊用
    public bool springFlag;


    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
    }
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
        HandleAttackInput(delta);
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


    private void HandleAttackInput(float delta)//新增一個攻擊的方法的Input
    {
        Rb_input = playerInput.PlayerAction.RB.triggered;
        Re_input = playerInput.PlayerAction.RR.triggered;
        Rf_input = playerInput.PlayerAction.RF.triggered;


        if(Rb_input)
        {
            if(playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttack.HandleAttackCombo(playerInventory.rightweapon);
                comboFlag = false;
            }
            else//普通攻擊沒有連續攻擊
            {
                if(playerManager.isInteracting)
                    return;//假如有連續攻擊的話，就不用再撥放第二攻擊
                if(playerManager.canDoCombo)
                    return;//假如有連續攻擊的話，就不用再撥放第二攻擊
                playerAttack.Handle_Heavy_Attack(playerInventory.rightweapon);
            }
        }

        if(Re_input)
        {
            playerAttack.Handle_Light_Attack(playerInventory.rightweapon);
        }

        if(Rf_input)
        {
            playerAttack.Handle_JumpAttack(playerInventory.rightweapon);
        }

    }
}
