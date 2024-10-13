using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class GameVFX : MonoBehaviour
{
    Sword_FX_Particle sword_FX_Particle;
    public GameObject[] particleSystems;
    DamageCollider damageCollider;
    public PlayerManager playerManager;
    [Header("相機震動")]
    public CinemachineImpulseSource impulse;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    
    void Start()
    {
        impulse =  cinemachineVirtualCamera.GetComponent<CinemachineImpulseSource>();
        damageCollider = FindObjectOfType<DamageCollider>();
        sword_FX_Particle = FindObjectOfType<Sword_FX_Particle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager.isInteracting && damageCollider.enermyIsHit)
            impulse.GenerateImpulse(Vector3.right);

        if(damageCollider.enermyIsHit)
        {
            GameObject obj = Instantiate(particleSystems[0], damageCollider.hitpoint, Quaternion.identity);
            Destroy(obj,0.2f);
            damageCollider.enermyIsHit=false;
        }

       if(damageCollider.playerIsHit)
        {
            sword_FX_Particle.Sword_01_Close();
            sword_FX_Particle.R_Attack_16_Close();
            sword_FX_Particle.R_Attack_06_Close();
            damageCollider.playerIsHit=false;
        }


    }
}
