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

    public bool canDoCombo;

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
        canDoCombo = anim.GetBool("canDoCombo");
        isInteracting = anim.GetBool("isInteracting");
    }

     void LateUpdate()
    {
        inputHandler.rollFlag=false;
        inputHandler.springFlag=false;
        inputHandler.Rb_input=false;
        inputHandler.Re_input=false;
        inputHandler.Rf_input=false;
        isSprinting = inputHandler.g_input;
    }



}
