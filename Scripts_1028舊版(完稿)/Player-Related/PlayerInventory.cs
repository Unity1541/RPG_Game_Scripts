using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

  WeaponSlotManager weaponSlotManager;  
  public WeaponItems rightweapon;
  public WeaponItems leftweapon;
  public WeaponItems  unarmedWeapon;
  [Header("The Array for Change_WeaponItem")]
  public WeaponItems[] weaponInRightHandSlots = new WeaponItems[1];
  public WeaponItems[] weaponInLeftHandSlots = new WeaponItems[1];
      //new WeaponItems[1]，它只有一個位置，即 weaponInRightHandSlots[0]，
      //這是陣列的第一個位置，因為索引從 0 開始。所以，這個陣列只能容納一個元素。
  public int currentRightWeaponIndex=-1;
  public int currentLeftWeaponIndex=-1;
  
  private void Awake()
  {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
      
  }

  private void Start()
  {
      //  //以下新增用陣列讀取武器
      //   rightweapon = weaponInLeftHandSlots[currentRightWeaponIndex];
      //   leftweapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
      //   //由PlayInventory開始，給WeaponSLotManager判斷左邊還是右邊
      //   weaponSlotManager.LoadWeaponOnSlot(rightweapon,false);//一開始右手沒東西
      //   weaponSlotManager.LoadWeaponOnSlot(leftweapon,true);         

      rightweapon = unarmedWeapon;
      leftweapon = unarmedWeapon;
  }

  //以下新增改變武器的方法
  public void ChangeRightWeapon()
  {
      currentRightWeaponIndex = currentRightWeaponIndex + 1;//一開始給定-1開始往上加
      if(currentRightWeaponIndex==0 && weaponInRightHandSlots[0]!=null)//只是確保list裡面至少要放入一個武器
      {
            rightweapon = weaponInRightHandSlots[currentRightWeaponIndex];//編號[0]取得第一個武器，還沒有被Instinate，下方才是生成prefab
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
      }
      else if (currentRightWeaponIndex ==0 && weaponInRightHandSlots[0]==null)
      {
            currentRightWeaponIndex = currentRightWeaponIndex+1;

      }

      else if(currentRightWeaponIndex==1 && weaponInRightHandSlots[1]!=null)//index要等於1至少要放入兩個武器才可以，不然會OutOfBoundry
      {
             rightweapon = weaponInRightHandSlots[currentRightWeaponIndex];
             weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
      }
      else
      {
            currentRightWeaponIndex = currentRightWeaponIndex+1;
      }

      if(currentRightWeaponIndex > weaponInRightHandSlots.Length-1)//假設武器已經到盡頭了，那就從頭開始
      {
            currentRightWeaponIndex = -1;
            rightweapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,false);
      }
  }

  public void ChangeLeftWeapon()
  {
      currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
      if(currentLeftWeaponIndex==0 && weaponInLeftHandSlots[0]!=null)
      {
            leftweapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
      }
      else if (currentLeftWeaponIndex ==0 && weaponInLeftHandSlots[0]==null)
      {
            currentLeftWeaponIndex = currentLeftWeaponIndex+1;

      }

      // else if(currentLeftWeaponIndex==1 && weaponInLeftHandSlots[1]!=null)//index要等於1至少要放入兩個武器才可以，不然會OutOfBoundry
      // {
      //        leftweapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
      //        weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
      // }
      else
      {
            currentLeftWeaponIndex = currentLeftWeaponIndex+1;
      }

      if(currentLeftWeaponIndex > weaponInLeftHandSlots.Length-1)//假設武器已經到盡頭了，那就從頭開始
      {
            currentLeftWeaponIndex = -1;
            leftweapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,true);
      }
  }


}
