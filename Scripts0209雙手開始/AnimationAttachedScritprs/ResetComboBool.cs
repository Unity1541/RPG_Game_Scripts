using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetComboBool : StateMachineBehaviour
{
    public string targetBool;
    public bool status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateIno, int layerIndex)
    {
        animator.SetBool(targetBool,status);
    }  
}
