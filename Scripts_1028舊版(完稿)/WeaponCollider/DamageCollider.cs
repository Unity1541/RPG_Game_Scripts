using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public float currentWeaponDamage=0.3f;

    void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        //damageCollider.isTrigger= true;用不到tirgger了，因為這邊改用Collision來偵測
        damageCollider.enabled = false;//平常關掉collider，等需要的時候再打開
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled=true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled=false;
    }

    //為了要有物理碰撞效果，這邊不用OnTriggeredEnter來做
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Enermy")
        {
            EnermyStatus enermyStatus = collision.gameObject.GetComponent<EnermyStatus>();
            if(enermyStatus != null)
            {
                enermyStatus.TakeDamage(currentWeaponDamage);
                Debug.Log("You are hitting a enermy");
            }
        }


         if(collision.gameObject.tag=="Player")
        {
            PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
            if(playerStatus != null)
            {
                playerStatus.TakeDamage(currentWeaponDamage);
            }
        }

    }

}
