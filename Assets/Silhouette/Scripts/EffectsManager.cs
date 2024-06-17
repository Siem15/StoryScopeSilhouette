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
    [SerializeField] private List<Material> materialsList;      //List of materials to be managed
    private Dictionary<string, Material> materialsDictionary;   //Dictionary to store materials with names as keys

    //Particle Systems
    [SerializeField] private GameObject FoodParticleSystem;

    //VisualFX
    [SerializeField] private GameObject InstantiationVFX;

    private void Awake()
    {
        // Initialize the dictionary
        materialsDictionary = new Dictionary<string, Material>();

        // Populate the dictionary with materials
        foreach (Material material in materialsList)
        {
            if (material != null)
            {
                materialsDictionary[material.name] = material;
            }
        }
    }
    private void Start()
    { 

    }

    private void FixedUpdate()
    {
        
    }

    // Method to get a material by name
    private Material GetMaterialByName(string materialName)
    {
        if (materialsDictionary.ContainsKey(materialName))
        {
            return materialsDictionary[materialName];
        }
        else
        {
            Debug.LogError("Material not found: " + materialName);
            return null;
        }
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
        if (caster == null)
        {
            Debug.LogError("Target GameObject is not assigned.");
            return;
        }

        // Get the material by name
        Material requestedMaterial = GetMaterialByName(ShaderName);

        if (requestedMaterial != null)
        {
            // Get the MeshRenderer component
            MeshRenderer meshRenderer = caster.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Assign the requested material
                meshRenderer.material = requestedMaterial;
            }
            else
            {
                Debug.LogError("MeshRenderer component is missing on the target GameObject.");
            }
        }
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
