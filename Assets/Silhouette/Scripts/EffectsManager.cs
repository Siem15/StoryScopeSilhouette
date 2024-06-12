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
    [SerializeField] private Material onPlacementEffectShader;
    [SerializeField] private Material onFireEffectShader;
    [SerializeField] private Material dissolveShader;

    [SerializeField] private GameObject FoodParticleSystem;

    [SerializeField] private GameObject InstantiationVFX;
    
    private Material CurrentObjectMaterial;

    private void Start()
    {
        //CurrentObjectMaterial = this.gameObject.GetComponent<Renderer>().material;
    }

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
        //transfer particle system naar nieuwe parent
        //set child position to current position of parent



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
