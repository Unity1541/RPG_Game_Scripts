using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_FX_Particle : MonoBehaviour
{
  public ParticleSystem[] particleSystemList;

  public void Sword_01_Open()
  {
     particleSystemList[0].Play();
  }

  public void Sword_01_Close()
  {
     particleSystemList[0].Stop();
  }

  public void R_Attack_06_Open()
  {
    particleSystemList[1].Play();
  }

  public void R_Attack_06_Close()
  {
    particleSystemList[1].Stop();
  }

  public void R_Attack_16_Open()
  {
    particleSystemList[2].Play();
  }

  public void R_Attack_16_Close()
  {
    particleSystemList[2].Stop();
  }

  public void R_Attack_14_Open()
  {
    particleSystemList[3].Play();
  }
  public void R_Attack_14_Close()
  {
    particleSystemList[3].Stop();
  }

  public void R_Attack_14_1_Open()
  {
    particleSystemList[4].Play();
  }
  public void R_Attack_14_1_Close()
  {
    particleSystemList[4].Stop();
  }
}
