using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MonoBehaviour代表自己有unity控制的方法，不是用new class來實作
public class PlayerLocomotion : MonoBehaviour
{
    public Transform cameraObject;
    InputHandler inputHandler;
    AnimationHandler animationHandler;
    public Vector3 moveDirection;
    public Transform myTransform;
    public new Rigidbody rigidbody;
    public bool isSprinting;
    
    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float rotationSpeed = 2f; 
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        myTransform = transform;
        animationHandler.Initialized();
    }


    public void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        HandleMovement(delta);
        HandleRollingAndSprinting(delta);
        isSprinting = inputHandler.g_input;
       
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;


    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.movementAmount;
        //targetDir = cameraObject.forward*inputHandler.vertical+cameraObject.right*inputHandler.horizontal;
        //這個代碼片段將垂直和水平的輸入值分別應用於相機的前方向和右方向，然後將它們相加以計算最終的目標方向。
        //這可以實現一種類似於第一人稱或第三人稱角色的移動，其中玩家可以同時控制前進/後退和左/右移動。
    //cameraObject.forward 不代表 (1, 0, 0)。cameraObject.forward 是一個向量，它表示相機的觀察方向，而不是一個特定的坐標。
    //在Unity中，cameraObject.forward 是一個獲取相機的正前方向（觀察方向）的屬性，這個向量是一個單位向量，
    //它指向相機所面向的方向。這個向量的具體數值取決於相機的旋轉和方向。
        //下面是將水平和垂直方向分開寫，也可以達到相同目標
        //這個代碼片段的作用也是計算目標方向，但它是分開計算垂直和水平移動，然後將它們相加。
        //結果是相同的，最終得到的 targetDir 向量是相同的。這種方法可能用於更清晰地分別處理垂直和水平移動。
        targetDir += cameraObject.forward*inputHandler.vertical;
        targetDir += cameraObject.right*inputHandler.horizontal;
        targetDir.Normalize();//這邊因為一開始設定，NewInputSystem的時候，就是用analogue,而不是用digitzlized，因此這邊再一次Normalize()
        targetDir.y=0;
            if(targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }
    //這樣的設計通常用於處理當玩家沒有按下任何移動按鈕時的情況。
    //如果 inputHandler.vertical 和 inputHandler.horizontal 都等於零，即玩家沒有按下任何移動按鈕，則 targetDir 會被設置為主角的前方向量，以確保主角保持面向某個方向。
        float rs = rotationSpeed;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr,5*delta);
        myTransform.rotation = targetRotation;
    }

    private void HandleMovement(float delta)
    {
        if(inputHandler.rollFlag)
            return;

        moveDirection = cameraObject.forward*inputHandler.vertical;
        moveDirection += cameraObject.right*inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y=0f;//避免人物越走越高
        float speed = movementSpeed;
        
        if(inputHandler.springFlag)
        {
             speed = 10;
             isSprinting=true;
             moveDirection *=speed;
        }
        else
        {
            
            moveDirection *=speed;
        }
           

        Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection,normalVector);
        //moveDirection 是你希望投影的原始向量，可能代表某個對象的運動方向。
        //normalVector 是代表平面的法向量，也就是指定的投影平面的方向。
        //Vector3.ProjectOnPlane 函數將 moveDirection 向量投影到由 normalVector 定義的平面上，
        //並返回投影後的向量。
        rigidbody.velocity = projectVelocity;
        //賦值給 rigidbody 的 velocity 屬性。它只是改變了物體的速度方向，而不是移動物體本身。
        //要使物體真正移動，通常需要應用一些力或施加一些作用力，物體才會根據其質量和受到的力而移動。
        //速度的改變只是影響物體的未來運動的一部分，但它本身不能直接導致物體的移動。
        //總結，該代碼片段只處理速度的方向，而不直接移動物體。
        //物體的移動仍然需要考慮物理模擬和適當的力或作用力。
        animationHandler.UpdateAminatorValues(inputHandler.movementAmount,0);
        if(animationHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    private void HandleRollingAndSprinting(float delta)
    {
         //if(animationHandler.anim.GetBool("isInteracting"));
            //return;//假設有做到其他動作，就不能滾動或衝刺了
        
        //if(inputHandler.rollFlag==true) 
        //if(inputHandler.rollFlag) 這兩種寫法在大多數情況下是等價的，因為它們都用於檢查 inputHandler.rollFlag 是否為 true。
        if(inputHandler.rollFlag)
        {
            moveDirection = cameraObject.forward*inputHandler.vertical;
            moveDirection += cameraObject.right*inputHandler.horizontal;
                if(inputHandler.movementAmount > 0)//有同時按下shift以及上下左右按鍵，才會變成滾動
                {
                    animationHandler.PlayerTargetAnimation("roll_front",true);
                    moveDirection.y=0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation= rollRotation;
                }
                else
                {
                    animationHandler.PlayerTargetAnimation("walk_back",true);

                }
        }
        if(inputHandler.springFlag)
        {
            animationHandler.PlayerTargetAnimation("R_Dash",true);
        }   
    }


    #endregion
}
