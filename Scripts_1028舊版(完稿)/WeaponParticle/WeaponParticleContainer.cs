using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParticleContainer : MonoBehaviour
{
     public ParticleSystem []particleSystems;

     public void Call_Element_0()
     {
        particleSystems[0].Play();
     }

     public void Stop_Element_0()
     {
        particleSystems[0].Stop();
     }

     public void Call_Element_1()
     {
       particleSystems[1].Play();
     }

     public void Stop_Element_1()
     {
       particleSystems[1].Stop();
     }

}
