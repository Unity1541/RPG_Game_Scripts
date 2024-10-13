using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPractice : MonoBehaviour
{
    public Transform playerTransform; // 主角的 Transform 就是player
    public Transform enemyTransform; // 敵人的 Transform 就是敵人
    //public float moveSpeed = 5.0f; // 移動速度
    public float turnDistance = 3.0f; // 轉向敵人的距離小於3就會開始轉向
    public float rotationSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
      Vector3 direction = enemyTransform.position - playerTransform.position;
        direction.y = 0.0f; // 將方向向量的 y 分量設為 0，使主角只在 x-z 平面上移動

        // 檢查距離是否在轉向範圍內
        float distance = direction.magnitude;//長度是指從原點（0,0,0）到向量所表示的點的距離。這個長度可以用來表示向量的大小或距離。是長度本身
        if (distance <= turnDistance)
        {
            // 面向敵人
            if (direction != Vector3.zero)//排除和敵人同一個位置
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction,Vector3.up);//繞著y軸旋轉
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        // 移動主角向敵人靠近
       // playerTransform.position += direction.normalized * Time.deltaTime * moveSpeed;  
    }
}
