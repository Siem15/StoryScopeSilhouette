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
    //Local Variables
    [SerializeField] private float waitTime;

    //Shaders
    [SerializeField] private GameObject fireStarter;
    [SerializeField] private Material DissolveShader;

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
        switch (ShaderName)
        {
            case "onFireEffectShader":
                GameObject Child_0 = Instantiate(fireStarter);
                MakeChildDoParentsThing(Child_0, caster);
                break;
            case "dissolveShader":
                List<GameObject> children = GetChildren(caster);
                foreach (var item in children)
                {
                    Material shaderGraphMaterial = DissolveShader;
                    Material oldMaterial;
                    SpriteRenderer spriteRenderer;
                    Renderer objectRenderer;

                    // Get the SpriteRenderer component from the current GameObject
                    spriteRenderer = item.GetComponent<SpriteRenderer>();

                    // Get the Renderer component (can be MeshRenderer or other types)
                    objectRenderer = item.GetComponent<Renderer>();


                    if (spriteRenderer != null && objectRenderer != null && shaderGraphMaterial != null)
                    {
                        oldMaterial = objectRenderer.material;
                        // Assign the ShaderGraph material to the object
                        objectRenderer.material = shaderGraphMaterial;

                        shaderGraphMaterial.SetFloat("ResetTime", Time.time);

                        // Get the texture from the SpriteRenderer's sprite
                        Texture2D spriteTexture = spriteRenderer.sprite.texture;

                        // Set the texture on the ShaderGraph material
                        objectRenderer.material.SetTexture("_MainTex", spriteTexture);
                        if (oldMaterial != null)
                        {
                            StartCoroutine(ResetOldMaterial(oldMaterial, objectRenderer));
                        }
                    }
                }
                break;

                //TODO: make the shader same size as object
        }
    }

    IEnumerator ResetOldMaterial(Material oldMaterial, Renderer objectRenderer)
    {
        yield return new WaitForSeconds(waitTime-2f);
        objectRenderer.material = oldMaterial;
    }

    private void AddParticleSystem(string ParticleSystemName, GameObject caster)
    {
        GameObject Child = Instantiate(FoodParticleSystem);
        MakeChildDoParentsThing(Child, caster);
        Destroy(Child, waitTime);
    }

    private void AddVisualFX(string VisualFXName, GameObject caster)
    {
        Vector3 oldPosition = caster.transform.position;
        GameObject VisualFX = Instantiate(InstantiationVFX, oldPosition, Quaternion.identity);
        Destroy(VisualFX, waitTime);
    }

    //TODO: explain the name and code
    private void MakeChildDoParentsThing(GameObject Child, GameObject caster)
    {
        Child.transform.parent = caster.transform;
        Child.transform.localPosition = Vector3.zero;
        Child.transform.localRotation = Quaternion.identity;
        Child.transform.localScale = Vector3.one;
    }

    //TODO: explain code
    public List<GameObject> GetChildren(GameObject caster)
    {
        List<GameObject> children = new List<GameObject>();
        AddChildren(caster.transform, children);
        return children;
    }

    private void AddChildren(Transform parent, List<GameObject> children)
    {
        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
            // Recursively add the children of this child
            AddChildren(child, children);
        }
    }
}
