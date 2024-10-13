using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermWeaponSlotManager : MonoBehaviour
{
   DamageCollider damageCollider;
   void Awake()
   {
        damageCollider = GetComponentInChildren<DamageCollider>();
   }

   public void CallDamageCollider()
   {
        damageCollider.EnableDamageCollider();
   }

   public void StopDamageCollider()
   {
       damageCollider.DisableDamageCollider();
   }
}
