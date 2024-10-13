using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageCollider : MonoBehaviour
{
        public bool enermyIsHit;//判斷製造打到敵人的粒子
        public bool playerIsHit;//判斷打到玩家後的後續
        public Vector3 hitpoint;
        Collider damageCollider;
        public float currentweaponDamage=0.3f;
        public Transform playerTransform;//因為這邊的damageCollider是資產，無法直接從場景拖曳，因此只能用Find

        void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.enabled = false;//平常關掉collider，等需要的時候再打開
            playerTransform = GameObject.Find("Character").transform;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled=true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled=false;
        }

        void OnTriggerEnter(Collider collider)
        {

            if (collider.tag =="Player")
            {
                playerIsHit=true;
                Debug.Log("打到玩家");
                PlayerStatus playerStatus = collider.gameObject.GetComponent<PlayerStatus>();
                playerStatus.TakeDamage(currentweaponDamage);
            }

            if (collider.tag =="Boss")
            {
                enermyIsHit=true;
                Debug.Log("打到敵人");
                EnermyStatus enermyStatus = collider.gameObject.GetComponent<EnermyStatus>();
                enermyStatus.TakeDamage(currentweaponDamage);
                hitpoint = collider.ClosestPointOnBounds(transform.position);
                //這邊因為不是用Collision，因此沒有contact[0],來找到第一個撞擊點，因此改用OnTriggerEnter的collider
                //內建的ClosetPoionBounds，效果沒有Collision好
                //Debug.Log("Closest Point: " + hitpoint);
                playerTransform.DOLookAt(collider.gameObject.transform.position,0.2f,AxisConstraint.None, Vector3.up);

            }
        }
}
