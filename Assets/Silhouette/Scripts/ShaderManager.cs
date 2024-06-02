using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this script materials are called on the moment that objects with the required properties interact with eachother.
/// 
/// - Siem Wesseling, 29/05/2024
/// </summary>

public class ShaderManager : MonoBehaviour
{
    [SerializeField] private Material onPlacementEffectShader;
    [SerializeField] private Material onFireEffectShader;
    [SerializeField] private Material dissolveShader;

    private Material CurrentObjectMaterial;

    private void Start()
    {
        CurrentObjectMaterial = this.gameObject.GetComponent<Renderer>().material;
    }

    //This function adds the specific shader that is called for in the propertyscript
    public void AddShader(string InteractionName)
    {
        switch (InteractionName)
        {
            case "OnPlacementEffectShader":
                CurrentObjectMaterial = onPlacementEffectShader;  //Add ripple effect upon placing object
                break;
            case "onFireEffectShader":
                CurrentObjectMaterial = onFireEffectShader;   //Add fire effect shader onto current object upon interaction
                break;
            case "dissolveShader":
                CurrentObjectMaterial = dissolveShader;   //Add dissolve effect upon having burned for x amount of time
                break;
        }
    }
}
