using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    
    private float damage =0.3f;
    // private void OnTriggerEnter(Collider collider)
    // {
    //     //collider.GetComponent<PlayerStatus>().TakeDamage(damage);這樣也可以直接寫不用另外命名變數

    //     PlayerStatus playerStatus = collider.GetComponent<PlayerStatus>();
    //     if(playerStatus != null)
    //     {
    //         playerStatus.TakeDamage(damage);
    //     }
    // }


    //改用碰撞方式來做，這樣本身就不用把triggered打勾，也可以加上物理碰撞效果

      private void OnCollisionEnter(Collision collision)
      {

          if(collision.gameObject.tag == "Player")
          {
            PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
            if(playerStatus != null)
            {
                playerStatus.TakeDamage(damage);
            }

          }
      }
}
