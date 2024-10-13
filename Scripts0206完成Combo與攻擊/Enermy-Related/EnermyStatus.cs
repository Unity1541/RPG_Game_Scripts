using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyStatus : MonoBehaviour
{
    
    Animator animator;
    public float healthLevel = 0.5f;
    
     void Start()
    {
       animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        
        animator.CrossFade("R_Damage_Left_02",0.2f);
        Debug.Log("打到敵人");
        healthLevel -=damage;
        if(healthLevel<0.0f)
        {
            animator.CrossFade("R_Knockdown_Back",0.2f);
            this.transform.Translate(Vector3.back * 2.0f);
        }
        
    }

}
