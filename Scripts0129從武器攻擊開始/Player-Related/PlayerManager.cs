using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerLocomotion playerLocomotion;
    Animator anim;
    public bool isInteracting;
    public bool isSprinting;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        
        isInteracting = anim.GetBool("isInteracting");
    }

     void LateUpdate()
    {
        inputHandler.rollFlag=false;
        inputHandler.springFlag=false;
        // inputHandler.rb_input=false;
        // inputHandler.re_input=false;
        isSprinting = inputHandler.g_input;
    }



}
