using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//MonoBehaviour代表自己有unity控制的方法，不是用new class來實作
public class PlayerLocomotion : MonoBehaviour
{
    public Transform cameraObject;
    InputHandler inputHandler;
    AnimationHandler animationHandler;
    PlayerManager playerManager;
    public Vector3 moveDirection;
    public Transform myTransform;
    public new Rigidbody rigidbody; 
    [Header("Ground & Air Status")]
    [SerializeField]
    float groundDetectionRayStartPosition=0.5f;
    //float miniumDistanceNeedToBeFall=0.5f;
    [SerializeField] 
    float groundDirectionRayDistance=0.2f;
    public LayerMask GroundCheck;
    public float inAirTimer;
    //用這個來判斷，現在是準備下樓，還是真的跳下去，若在空中很久，則代表是要跳下來，如果只是很短表示下樓
    public float checkedRadius;
    public float GroundedOffset = -0.14f;

     [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f; 
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;



    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float rotationSpeed = 2f;
    [SerializeField]
    float fallingSpeed=150f; 
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        playerManager = GetComponent<PlayerManager>();
        myTransform = transform;
        animationHandler.Initialized();

        // playerManager.isGrounded=true;//一開始當然是在地表上
           playerManager.isInAir=false;
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
    //如果 inputHandler.vertical 和 inputHandler.horizontal 都等於零，即玩家沒有按下任何移動按鈕，
    //則 targetDir 會被設置為主角的前方向量，以確保主角保持面向某個方向。
        float rs = rotationSpeed;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr,5*delta);
        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta)
    {
        if(inputHandler.rollFlag)
            return;
        if(playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward*inputHandler.vertical;
        moveDirection += cameraObject.right*inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y=0f;//避免人物越走越高
        float speed = movementSpeed;
        
        if(inputHandler.springFlag)
        {
             speed = 10;
             playerManager.isSprinting=true;
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

    public void HandleRollingAndSprinting(float delta)
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
            if(playerManager.isInAir)
                    return;
            playerManager.isSprinting=true;
            inAirTimer=0.0f;
            animationHandler.PlayerTargetAnimation("R_Dash",true);      
        }   
    }


//     public void HandleFalling(float delta, Vector3 moveDirection)
//     {
//         playerManager.isGrounded=false;
//         //偵測RayCast
//         RaycastHit rayCastHit;
//         Vector3 origin = myTransform.position;
//         origin.y+=groundDetectionRayStartPosition;

//         if(Physics.Raycast(origin,myTransform.forward,out rayCastHit, 0.4f))
//         {
//             moveDirection = Vector3.zero;
//         }

//         if(playerManager.isInAir)
//         {
//             rigidbody.AddForce(-Vector3.up*fallingSpeed);//處裡空中掉下來
//             rigidbody.AddForce(moveDirection*fallingSpeed/5f);//處裡從懸崖直接掉下來
//         }

//         Vector3 dir = moveDirection;
//         dir.Normalize();
//         origin = origin + dir*groundDirectionRayDistance;
//         targetPosition = myTransform.position;

//         Debug.DrawRay(origin,-Vector3.up*miniumDistanceNeedToBeFall,Color.red,0.1f,false);
//         Debug.DrawRay(origin,myTransform.forward*2,Color.blue);
// //0.1：射線的持續時間，以秒為單位。這表示射線將在場景視圖中保持0.1秒的時間，然後消失。這個值通常用於控制射線的可見性時間。
// //false：一個布林值，表示射線是否繪製在最上層（true）或在最下層（false）。
// //當設置為 false 時，射線將被繪製在最上層，即在其他場景元素的上方。如果設置為 true，則射線將被繪製在最下層，即在其他場景元素的下方。
// //通常情況下，設置為 false 是常見的，以確保射線能夠在其他元素之上顯示出來，以便更容易觀察。
//         if(Physics.Raycast(origin,-Vector3.up*miniumDistanceNeedToBeFall,out rayCastHit,GroundCheck))
//         {
//             normalVector=rayCastHit.normal;
//             Vector3 tp = rayCastHit.point;
//             playerManager.isGrounded = true;
//             targetPosition.y = tp.y;

//                 if(playerManager.isInAir)
//                 {
//                     if(inAirTimer > 0.5f)
//                     {
//                         Debug.Log("You are in the Ground"+inAirTimer);
//                         animationHandler.PlayerTargetAnimation("LandSoft",true);
//                         inAirTimer=0;
//                     }
//                     else
//                     {
//                         animationHandler.PlayerTargetAnimation("MoveMent",false);
//                         inAirTimer=0;
//                     }
//                     playerManager.isInAir=false;
//                 }
//         }
//         else
//         {
//             if(playerManager.isGrounded)
//             {
//                 playerManager.isGrounded=false;
//             }
//             if(playerManager.isInAir==false)
//             {
//                 if(playerManager.isInteracting==false);
//                 {
//                     animationHandler.PlayerTargetAnimation("Falling",true);
//                 }
//                 Vector3 vel = rigidbody.velocity;
//                 vel.Normalize();
//                 rigidbody.velocity=vel * (movementSpeed/2);
//                 playerManager.isInAir=true;
//             }
//         }

//         if(playerManager.isGrounded)
//         {
//             if(playerManager.isInteracting || inputHandler.movementAmount > 0)
//             {
//                 //|| 是邏輯 OR 運算符，用於結合兩個布林表達式。它的作用是在兩個表達式中的任何一個為真（true）時
//                 //整個表達式將被評估為真。
//                 myTransform.position = Vector3.Lerp(myTransform.position,targetPosition,Time.deltaTime);
//             }
//             else
//             {
//                 myTransform.position = targetPosition;
//             }
//         }

//     }
    
    
     //public void HandleFalling(float delta, Vector3 moveDirecition)
    // {
    //     RaycastHit rayCastHit;
    //     Vector3 origin = myTransform.position;
    //     origin.y+=groundDetectionRayStartPosition;//這樣來手動調整raycast起始點，比較方便
    //     playerManager.isGrounded=Physics.CheckSphere(origin,checkedRadius,GroundCheck);
    //     //偵測地面與否
    //     if(playerManager.isGrounded)
    //     {
    //           print("play you are on the ground");
    //           playerManager.isInAir=false;
    //           playerManager.isGrounded=true;
    //           if( inAirTimer > 0.3f)
    //           //這段時間邏輯是說，在地面中有兩種情況，第一種是本來就在地面上正常情況，
    //           //另一種是說，從空中掉下來，回到地面上，此時要進入下面的迴圈，表示在空中停留超過0.5f以上
    //           //掉在地面的時候，要撥放landsoft表示
    //           {
    //                   playerManager.isInAir=false;
    //                   animationHandler.PlayerTargetAnimation("LandSoft",true);
    //                   inAirTimer=0.0f;
                 
    //           }       
    //     }
    //     else//這段是直接從高處掉下來，或者是走路懸空
    //     {
    //         playerManager.isInAir=true;
    //         //playerManager.isGrounded=false; 
    //         if(playerManager.isInAir)
    //         {
    //             if(inAirTimer < 0.1f)// && inputHandler.movementAmount>0
    //             {
    //                 rigidbody.AddForce(-Vector3.up*20,ForceMode.Impulse);
    //                 inAirTimer=0.0f;

    //             }
    //             if(inAirTimer > 0.18f)
    //             {
    //                 print("play you are not on the ground");
    //                 rigidbody.AddForce(-Vector3.up*20,ForceMode.Impulse);//處裡空中掉下來
    //                 //如果不加上 ForceMode.Impulse，也可以只是這樣掉下來比較""沒有感覺"""
    //                 animationHandler.PlayerTargetAnimation("Falling",true);
                    
                      
    //             }    
    //         }          
    //     }
      
    // }




    public void HandleFalling(float delta, Vector3 moveDirecition)
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,transform.position.z);
        playerManager.isGrounded = Physics.CheckSphere(spherePosition, checkedRadius, GroundCheck,QueryTriggerInteraction.Ignore);
        if(playerManager.isGrounded)
        {
            _fallTimeoutDelta = FallTimeout;
        }
        else
        {
            rigidbody.AddForce(-Vector3.up*33,ForceMode.Impulse);
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {   
                print("fall"+ _fallTimeoutDelta );
                animationHandler.PlayerTargetAnimation("Falling",true);
                    if(playerManager.isGrounded)
                    {
                        animationHandler.PlayerTargetAnimation("LandSoft",true);
                    }   
            }
        }

    }
    
    #endregion
}
