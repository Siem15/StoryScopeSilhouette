using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this script materials are called on the moment that objects with the required properties interact with eachother.
/// 
/// - Siem Wesseling, 29/05/2024
/// </summary>

public class EffectsManager : MonoBehaviour
{
    //Shaders

    //Particle Systems
    [SerializeField] private GameObject FoodParticleSystem;

    //VisualFX
    [SerializeField] private GameObject InstantiationVFX;

    //This function adds the specific shader that is called for in the propertyscript
    public void AddEffect(string InteractionName, GameObject caster)
    {
        switch (InteractionName)
        {
            case "onFireEffectShader":
                AddShader(InteractionName, caster);
                break;
            case "dissolveShader":
                AddShader(InteractionName, caster);
                break;
            case "GetsEaten":
                AddParticleSystem(InteractionName, caster);
                break;
            case "Removed":
                AddVisualFX(InteractionName, caster);
                break;
        }
    }

    private void AddShader(string ShaderName, GameObject caster)
    {

    }

    private void AddParticleSystem(string ParticleSystemName, GameObject caster)
    {
        GameObject Child = Instantiate(FoodParticleSystem);
        Child.transform.parent = caster.transform;
        Child.transform.localPosition = Vector3.zero;
        Child.transform.localRotation = Quaternion.identity;
        Child.transform.localScale = Vector3.one;

    }

    private void AddVisualFX(string VisualFXName, GameObject caster)
    {
        GameObject Child = Instantiate(InstantiationVFX);
        Child.transform.parent = caster.transform;
        Child.transform.localPosition = Vector3.zero;
        Child.transform.localRotation = Quaternion.identity;
        Child.transform.localScale = Vector3.one;
    }
}
