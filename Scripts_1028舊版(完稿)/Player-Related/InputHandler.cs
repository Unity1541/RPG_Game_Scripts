using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float movementAmount;
    public PlayerInput playerInput;
    public PlayerAttack playerAttack;
    public PlayerInventory playerInventory;
    public PlayerManager playerManager;
    public AnimationHandler animationHandler;
    public Vector2 movementInput;
    public bool b_input;//預設值=false
    public bool rollFlag;
    public bool springFlag;
    public bool comboFlag;
    public bool g_input;//給衝刺用
    public bool rt_input;//攻擊用
    public bool rb_input;//攻擊用
    public bool aim_input;//弓箭瞄準用
    public bool leftHandChange_input;
    public bool rightHandChange_input;


    private void Awake()
    {
        playerAttack =GetComponent<PlayerAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        animationHandler = GetComponentInChildren<AnimationHandler>();

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
        HandleAimInput(delta);
        HandleQuickSlotChangeWeapon();
    }
    public void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));
    }

    public void HandleRollMovement(float delta)
    {
        b_input=  playerInput.PlayerAction.Roll.triggered;
        if (b_input)
        {
            rollFlag=true;
        }
    }

    public void HandleSprintMovement(float delta)
    {
        g_input= playerInput.PlayerAction.Sprint.triggered;
        if(g_input)
        {
            springFlag=true;
        }
    }

    public void HandleAttackInput(float delta)
    {
        rt_input = playerInput.PlayerAction.RT.triggered;
        rb_input = playerInput.PlayerAction.RB.triggered;
        if(rt_input)
        {

            if(playerManager.canDoCombo)
            {
                comboFlag=true;
                playerAttack.HandleWeaponCombo(playerInventory.rightweapon);
                comboFlag=false;
            }
            else//沒有連續攻擊
            {
                if(playerManager.isInteracting)
                return;//假如有連續攻擊的話，就不用再撥放第二攻擊
                if(playerManager.canDoCombo)
                return;//假如有連續攻擊的話，就不用再撥放第二攻擊

                playerAttack.HandleLightAttack(playerInventory.rightweapon);
            }
            
        }

        if(rb_input)
        {
            playerAttack.HandleHeavyAttack(playerInventory.rightweapon);
        }
    }


    public void HandleAimInput(float delta)
    {
        if(Input.GetMouseButton(1)&&playerManager.isGrounded)//要用GetMouseButton(1)才有長按住效果
        {
            animationHandler.anim.SetBool("isAiming",true);
    
        }
        else
        {
           animationHandler.anim.SetBool("isAiming",false);
        }
    }

    public void HandleQuickSlotChangeWeapon()
    {
        rightHandChange_input = playerInput.PlayerChangeWeapon.RightHand.triggered;
        leftHandChange_input = playerInput.PlayerChangeWeapon.LeftHand.triggered;
        if(rightHandChange_input)
        {
            playerInventory.ChangeRightWeapon();
        }
        else if(leftHandChange_input)
        {
            playerInventory.ChangeLeftWeapon();
        }
    }

    


}
