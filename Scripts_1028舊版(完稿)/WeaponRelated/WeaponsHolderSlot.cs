using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolderSlot : MonoBehaviour
{
   public Transform parentOverride;//武器出現的定位點，通常是在右手或者是左手直接拖曳
   public bool isLeftHandSlot;
   public bool isRightHandSlot;

   public GameObject currentWeaponModel;
   

   public void UnLoadWeapon()
   {
        if(currentWeaponModel!=null)
        {
                currentWeaponModel.SetActive(false);
        }
   }


   public void UnloadWeaponAndDestroy()
   {
        if(currentWeaponModel!=null)
        {
            Destroy(currentWeaponModel);
        }
   }


   public void LoadWeaponModel(WeaponItems weaponItems)
   {
        UnloadWeaponAndDestroy();
        
        if(weaponItems==null)//就卸下武器
        {
            UnLoadWeapon();
            return;
        }
        GameObject model = Instantiate(weaponItems.prefabsModel,parentOverride) as GameObject;
        //第二個參數放位置
        
        //if(model != null)
        //{
        //     if(parentOverride!=null)
        //     {
        //         model.transform.parent=parentOverride;
        //     }
        //     else
        //     {
        //         model.transform.parent=transform;
        //     }
        //     model.transform.localPosition = Vector3.zero;
        //     model.transform.localRotation = Quaternion.identity;
        //     model.transform.localScale = Vector3.one;

        //}
         currentWeaponModel = model;

       
   }
}
