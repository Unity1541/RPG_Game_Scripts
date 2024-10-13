using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationBool : StateMachineBehaviour
{
    public string targetBool;
    public bool status;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layIndex)
    {
        animator.SetBool(targetBool,status);//如果是public但是沒有勾選，那就是預設值為false
        //這表示，進入Empty之後，就把isInteracting設為false
        //這跟離開原來動畫把inInteracting = false是一樣的道理
        //一個是進入emtpy設為false
        //一個是離開當下動畫後，設定為false
    }
}
