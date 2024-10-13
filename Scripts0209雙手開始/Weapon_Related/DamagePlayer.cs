using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
  
    public float damage =0.3f;
    private void OnCollisionEnter(Collision collision) //為了要有物理碰撞效果，這邊不用OnTriggeredEnter來做
    {
        PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
             playerStatus.TakeDamage(damage);
        }
       

    }
}
