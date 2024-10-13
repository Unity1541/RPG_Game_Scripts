using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnableController : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponParticleContainer weaponParticleContainer;

    void Start()
    {
        weaponParticleContainer= FindObjectOfType<WeaponParticleContainer>();
    }

    public void Call_Element_0_Particle()
    {
        weaponParticleContainer.Call_Element_0();
    }

    public void Stop_Element_0_Particle()
    {
        weaponParticleContainer.Stop_Element_0();
    }

     public void Call_Element_1_Particle()
    {
        weaponParticleContainer.Call_Element_1();
    }

    public void Stop_Element_1_Particle()
    {
        weaponParticleContainer.Stop_Element_1();
    }


}
