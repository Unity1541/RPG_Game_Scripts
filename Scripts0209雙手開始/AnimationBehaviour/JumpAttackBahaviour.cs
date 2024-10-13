using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class JumpAttackBahaviour : StateMachineBehaviour
{
    public float animation_Stop_Time;//負責暫停幾秒
    public float time_for_Scale;//收集動畫撥放的時間點，設定public方便觀看調整
    
    [Header("偵測第一個敵人的Collider")]
    public Collider[] colliders;
    public float detectRadius=2.5f;//偵測半徑大小，越大偵測越多東西
    public Transform playerTransform;
    public LayerMask layerMaskEnemy;
    [Header("跟敵人位置相關")]
    public Transform enemyTransform;
    public Vector3 direction; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         
         playerTransform = animator.transform.parent;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       colliders = Physics.OverlapSphere(playerTransform.position,detectRadius,layerMaskEnemy);

        for(int i=0;i<colliders.Length;i++)//回圈loop，例如有五個，index 從0~4
        {
            enemyTransform = colliders[i].gameObject.transform;
            Debug.Log("GetCollider"+colliders[i]+colliders[i].gameObject.name);
        }


       time_for_Scale = stateInfo.normalizedTime;
       direction = (enemyTransform.position - playerTransform.position).normalized;
       Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

      
       
       
        playerTransform.DORotateQuaternion(targetRotation, 0.5f).OnComplete(() =>
        {
                Debug.Log("動畫Over");
        });
    
         
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
