using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    AnimationHandler animationHandler;
    InputHandler inputHandler;
    public string lastAttack;

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    public void HandleAttackCombo(WeaponItems weaponItems)
    {
        if(inputHandler.comboFlag)
        {
            animationHandler.anim.SetBool("canDoCombo",false);
                if (lastAttack == weaponItems.OH_Heavy_Attack01)
                {
                animationHandler.PlayerTargetAnimation(weaponItems.OH_Light_Attack01,true);
                }
        }
       
    }
    public void Handle_Light_Attack(WeaponItems weaponItems)
    {
        animationHandler.PlayerTargetAnimation(weaponItems.OH_Light_Attack01,true);
        lastAttack = weaponItems.OH_Light_Attack01;//紀錄攻擊名稱
    }

    public void Handle_Heavy_Attack(WeaponItems weapnItems)
    {
        animationHandler.PlayerTargetAnimation(weapnItems.OH_Heavy_Attack01,true);
        lastAttack = weapnItems.OH_Heavy_Attack01;//紀錄攻擊名稱
    }

    public void Handle_JumpAttack(WeaponItems weapnItems)
    {
        animationHandler.PlayerTargetAnimation(weapnItems.OH_Jump_Attack01,true);
        lastAttack = weapnItems.OH_Jump_Attack01;//紀錄攻擊名稱
    }
}
