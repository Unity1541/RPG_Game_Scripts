using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerLocomotion playerLocomotion;
    Animator anim;
    public bool isInteracting;
    [Header("PlayerFlags")]
    public bool isInAir;
    public bool isGrounded;
    public bool isSprinting;
    public bool canDoCombo;
    public bool isAiming;
       void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta  = Time.deltaTime;
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isAiming = anim.GetBool("isAiming");
        inputHandler.TickInput(delta);//包含更新攻擊方法
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta,playerLocomotion.moveDirection);           
        
    }

    void LateUpdate()//不可用在FixUpdate不然不會更新，也可以放在Update就好，感覺不太出來
    {
        inputHandler.rollFlag=false;
        inputHandler.springFlag=false;
        isSprinting = inputHandler.g_input;
        inputHandler.rb_input=false;
        inputHandler.rt_input=false;
        inputHandler.leftHandChange_input=false;
        inputHandler.rightHandChange_input=false;
        //下面處理在空中的資訊
        if(isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer+Time.deltaTime;
        }
    }

}
