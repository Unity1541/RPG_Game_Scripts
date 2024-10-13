using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    AnimationHandler animationHandler;
    public ProgressBarPro progressBarPro;//取得外部的Healthbar把它拉入關係
    [Range(0f, 1f)]//下方固定0~1f
    public float healthLevel = 1f;
    
    void Start()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    public void TakeDamage(float damage)
    {
        healthLevel = healthLevel - damage;
        progressBarPro.Value = healthLevel;
        animationHandler.PlayerTargetAnimation("Damage2",true);

        if (healthLevel < 0f)
        {
            animationHandler.PlayerTargetAnimation("Shield-Death1",true);
            Debug.Log("玩家死亡!!");
            healthLevel = 0.0f;
            progressBarPro.Value =healthLevel;
            
        }
        
        
    }
    

}
