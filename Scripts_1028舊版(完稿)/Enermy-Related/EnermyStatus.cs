using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyStatus : MonoBehaviour
{
    // Start is called before the first frame update
  private float healthLevel=1f;
  private float currentHealth=1f;
  public Animator animator;
  public ProgressBarPro progressBarPro;//取得外部的Healthbar把它拉入關係

  void Awake()
  {
    progressBarPro.Value = healthLevel;
    animator = GetComponent<Animator>();
  }

  public void TakeDamage(float damage)
  {
    currentHealth = currentHealth - damage;
    progressBarPro.Value=currentHealth;
   
    if(currentHealth<=0)
    {
        animator.SetTrigger("Dying");
        this.GetComponent<Collider>().enabled=false;
        //死亡後就關掉collider以免死後鞭屍
    }
    else
    {
      animator.SetTrigger("Hit");
    }

  }

}
