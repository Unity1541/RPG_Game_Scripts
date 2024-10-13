using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
  public Animator anim;
  public InputHandler inputHandler;
  public PlayerLocomotion playerLocomotion;
  public PlayerManager playerManager;
  int vertical;
  int horizontal;
  public bool canRotate;//若沒有給值，一開始是false

  public void Initialized()//不會自動執行，要等著被呼叫
  {
    anim = GetComponent<Animator>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerLocomotion = GetComponentInParent<PlayerLocomotion>();
    playerManager = GetComponentInParent<PlayerManager>();
    vertical = Animator.StringToHash("Vertical");
    horizontal = Animator.StringToHash("Horizontal");
  }

  public void UpdateAminatorValues(float verticalMovement, float horizontalMovement)
  {
        #region Vertical
        float v=0;

        if(verticalMovement>0 && verticalMovement<0.55)
        {
          v=0.5f;
        }
        else if(verticalMovement>0.55)
        {
          v=1f;
        }
        else if(verticalMovement<0 && verticalMovement>-0.55)
        {
          v=-0.5f;
        }
        else if(verticalMovement<-0.55)
        {
          v=-1;
        }
        else
        {
          v=0;
        }
        #endregion

        #region  Horizontal
        float h=0;

        if(horizontalMovement>0 && horizontalMovement<0.55)
        {
          h=0.5f;
        }
        else if(horizontalMovement>0.55)
        {
          h=1f;
        }
        else if(horizontalMovement<0 && horizontalMovement>-0.55)
        {
          h=-0.5f;
        }
        else if(horizontalMovement<-0.55)
        {
          h=-1;
        }
        else
        {
          h=0;
        }
        #endregion



     anim.SetFloat(vertical,v,0.1f,Time.deltaTime);
     anim.SetFloat(horizontal ,h,0.1f,Time.deltaTime); 
  }


  public void PlayerTargetAnimation(string targetAim, bool isInteracting)
  {
    anim.applyRootMotion = isInteracting;//只有在isInterAction是True情況下，才需要applyRootMotion;
    anim.SetBool("isInteracting",isInteracting);
    anim.CrossFade(targetAim,0.2f);
  }

  public void CanRotate()
  {
    canRotate = true;
  }

  public void StopRotate()
  {
    canRotate = false;
  }

  public void OnAnimatorMove()
  {
      //這段會跟ani.applyRootMotion有關，是unity內建的函數
    //加上去後會覆蓋掉Animator的Apply Root InMotion選項
    //在使用OnAnimationMove之前，要先確定角色的Animator的apply.RootMotion要等於true
        if(playerManager.isInteracting==false)// playerManager.isInteracting==false，也可以差別在於，Root Motion會不會跟著動畫跑，playerManager的會跟著跑
        {//注意playerManager的參數是isInteracting, 但是inputHandler的參數是IsInteracting兩者不同
            return;//一但執行這邊，後面的甚麼float delta之後都不會再執行了
        }
            
            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
        //將 rigidbody.drag 設置為 0 可以取消物體在空氣中的阻力，這樣它的運動速度就不會因為空氣阻力而減慢。
            Vector3 deltaPosition =anim.deltaPosition;
        //使用上面的anim.deltaPosition之前，要先確認applyRootInMotion是有打勾的，才可以執行    
            deltaPosition.y =0;
            Vector3 velocity = deltaPosition/delta;
            playerLocomotion.rigidbody.velocity=velocity;
        //如果 playerManager.isInteracting 為 true，則根據Animator的根運動來更新角色的位置。
        //在這段程式碼中，首先檢查了 anim.deltaPosition，它是從Animator中獲取的角色在這一帧中的位移。然後，將這個位移向量的 y 分量設置為 0，
        //這樣可以確保角色只在水平面上移動。
        //接著，計算出角色的速度，並將這個速度應用到角色的 Rigidbody 上，從而讓角色根據動畫的根運動移動。
        //這段程式碼確保了在動畫具有根運動時，角色會依據動畫的移動來移動
        //如果該動畫沒有根運動（Root Motion），這段程式碼將不會產生影響。因為這段程式碼是根據Animator的根運動來更新角色的位置，如果動畫沒有根運動，則 anim.deltaPosition 將為零，
        //因此角色的速度也將為零，最終角色的位置不會發生變化。   
    
  }

  public void EnableCombo()
  {
      anim.SetBool("canDoCombo",true);
  }

  public void DisableCombo()
  {
    anim.SetBool("canDoCombo",false);
  }
}
