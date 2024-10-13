using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    AnimationHandler animationHandler;
    InputHandler inputHandler;
    public string lastAttack;//處裡連續攻擊的參數

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    public void HandleWeaponCombo(WeaponItems weaponItems)
    {
        if(inputHandler.comboFlag)
        {
            animationHandler.anim.SetBool("canDoCombo",false);//一旦進入連續攻擊之後，就馬上設定回預設值
            if(lastAttack == weaponItems.One_Light_Attack_01)
            {
                animationHandler.PlayerTargetAnimation(weaponItems.Two_Heavy_Attack_01,true);
            }

        }

       
    }
    public void HandleLightAttack(WeaponItems weaponItems)//個別攻擊
    {//注意這邊的動畫名稱已經是string了，不必再加上"",另外名稱要和動畫名稱一樣
       animationHandler.PlayerTargetAnimation(weaponItems.One_Light_Attack_01,true);
       lastAttack = weaponItems.One_Light_Attack_01;//讀取目前攻擊的名稱，來當作是連續攻擊的參考招式
    }

    public void HandleHeavyAttack(WeaponItems weaponItems)//個別攻擊
    {
       animationHandler.PlayerTargetAnimation(weaponItems.Two_Heavy_Attack_01,true);
       lastAttack = weaponItems.Two_Heavy_Attack_01;
    }


    public void HandleAimAttack()
    {
        
    }

}
