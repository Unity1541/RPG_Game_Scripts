using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_FX_Particle : MonoBehaviour
{
    // Start is called before the first frame update
  public ParticleSystem particleSystemSword01;
  public ParticleSystem particleSystemSword02;
  public ParticleSystem particleSystemSword03;

  public void Sword_01_Open()
  {
     particleSystemSword01.Play();
  }

  public void Sword_01_Close()
  {
    particleSystemSword01.Stop();
  }

  public void R_Attack_06_Open()
  {
    particleSystemSword02.Play();
  }

  public void R_Attack_06_Close()
  {
    particleSystemSword02.Stop();
  }

  public void R_Attack_16_Open()
  {
    particleSystemSword03.Play();
  }

  public void R_Attack_16_Close()
  {
    particleSystemSword03.Stop();
  }
}
