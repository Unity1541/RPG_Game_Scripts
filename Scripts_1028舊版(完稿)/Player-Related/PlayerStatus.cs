using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
  private float healthLevel=1f;
  private float currentHealth=1f;
  AnimationHandler animationHandler;
  public ProgressBarPro progressBarPro;//取得外部的Healthbar把它拉入關係

  void Awake()
  {
    progressBarPro.Value = healthLevel;
    animationHandler = GetComponentInChildren<AnimationHandler>();
  }

  public void TakeDamage(float damage)
  {
    currentHealth = currentHealth - damage;
    progressBarPro.Value=currentHealth;
    animationHandler.PlayerTargetAnimation("Sword_Block_Right_Shield",true);
    if(currentHealth<=0)
    {
        animationHandler.PlayerTargetAnimation("dead_05",true);
    }
  }


}
