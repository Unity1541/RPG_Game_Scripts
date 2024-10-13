using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponShoot : MonoBehaviour
{
   
    public GameObject []Weapons;
    public Transform playerPosition;
    public Transform shakePosition;//這邊要是Virtual Camera追蹤對象，不然不會震動，只是該物件自己在動
    public Transform []target;
    public Transform arrowShootPosition;
    public Vector3 modify_Instinate_Position;
    public float launchSpeed = 100f; // 
    GameObject cloneWeaponOfArrow;
    GameObject cloneMagicCircle;

    void Start()
    {
       // Weapons[0].transform.localRotation = Quaternion.Euler(0,arrowShootPosition.transform.rotation.eulerAngles.y, 0);
    }
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        { 
            
            cloneMagicCircle = Instantiate(Weapons[1],arrowShootPosition.position+modify_Instinate_Position, arrowShootPosition.rotation);
        }


        if (Input.GetMouseButtonUp(1))
        {
           shakePosition.DOShakePosition(duration: 0.2f, strength: new Vector3(0.1f, 0.1f, 0.2f), vibrato: 3, randomness: 90, fadeOut: true);
           cloneWeaponOfArrow = Instantiate(Weapons[0], arrowShootPosition.position,arrowShootPosition.rotation);
           Destroy(cloneMagicCircle,0.2f);//3秒後摧毀  
           Destroy(cloneWeaponOfArrow,3f);//3秒後摧毀     
        }
        
    }

    
}
