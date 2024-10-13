using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Item/Weapon Items")]
public class WeaponItems : Item
{
    public GameObject prefabsModel;
    public bool isUnarmed;
    [Header("Attack Shield Animations")]
    public string One_Light_Attack_01;
    public string Two_Heavy_Attack_01;
    public string Shield_01;
   
    [Header("Idle Animation")]
    public string leftHandIdle;
    public string rightHandIdle;
}
