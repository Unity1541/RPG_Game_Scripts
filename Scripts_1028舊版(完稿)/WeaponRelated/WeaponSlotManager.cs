using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
  
    WeaponsHolderSlot leftHandSlot;
    WeaponsHolderSlot rightHandSlot;

    DamageCollider leftHandDamagerCollier;
    DamageCollider rightHandDamagerCollier;

    public void Awake()//一開始左右手都已經勾選好了，也可以只勾選一手
    {
        WeaponsHolderSlot[] weaponsHolderSlots = GetComponentsInChildren<WeaponsHolderSlot>();
        //因為左右手都會放入WeaponHolderSlot因此要用複數s
        //注意有加上Components-->s復數，這樣才可以變陣列
        foreach(WeaponsHolderSlot weaponslot in weaponsHolderSlots)//注意這邊塞的是程式碼
        {
            if(weaponslot.isLeftHandSlot)
            {//注意這邊的isLeftHandSLot，一開始Scene場景上面，裝在武器的位置上就要自己(左右手預先)勾選才可以進行
                leftHandSlot = weaponslot;
            }
            else if(weaponslot.isRightHandSlot)//注意這邊的isRightHandSLot，一開始Scene場景上面就要自己勾選才可以進行
            {//預設值我們給右邊先勾選
                rightHandSlot = weaponslot;//這邊代表把weaponslot的程式碼塞給rightHandSlot
            }
        } 
    }

    //決定好左右手，哪邊有武器之後，接下來就是讀取武器
    public void LoadWeaponOnSlot(WeaponItems weaponItems,bool isLeft)
    {//左邊右邊由PlayerInventory來決定
        if(isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItems);//讀取武器後，順便讀取該武器的collider
            //LoadLeftWeaponDamageCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItems);
            LoadRightWeapinDamageCollider();
        }
    }

    // public void LoadLeftWeaponDamageCollider()//目前只有右手，先把左手關掉
    // {
    //     leftHandDamagerCollier = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    // }


#region 跟collider有關的，左手部分還沒弄
    private void LoadRightWeapinDamageCollider()
    {
        rightHandDamagerCollier = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

    }

    public void OpenRightHandDamageCollider()
    {
        rightHandDamagerCollier.EnableDamageCollider();
    }

    public void CloseRightHandDamageCollider()
    {
        rightHandDamagerCollier.DisableDamageCollider();
    }
#endregion

}
