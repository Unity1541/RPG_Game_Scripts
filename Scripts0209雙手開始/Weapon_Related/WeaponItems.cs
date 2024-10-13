using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/WeaponItem")]
public class WeaponItems : Items
{
   //繼承Items的屬性之外，還可以有自己的屬性
   public GameObject modelPrefab;
   public bool isUnarmed;

   [Header("One Hand Attack Animation")]
   public string OH_Light_Attack01;
   public string OH_Heavy_Attack01;

   public string OH_Jump_Attack01;
   public string OH_Jump_Attack02;

}
