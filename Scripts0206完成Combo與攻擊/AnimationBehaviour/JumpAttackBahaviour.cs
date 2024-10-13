using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class JumpAttackBahaviour : StateMachineBehaviour
{
    public float animationTime;//負責暫停幾秒
    public float time_for_Scale;//收集動畫撥放的時間點，設定public方便觀看調整
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       time_for_Scale = stateInfo.normalizedTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //    // Implement code that processes and affects root motion
    // }

    // override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //    // Implement code that sets up animation IK (inverse kinematics)
    // }
}
