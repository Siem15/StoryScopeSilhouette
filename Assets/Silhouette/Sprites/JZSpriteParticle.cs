using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSpriteParticle : MonoBehaviour
{
    Material particleMaterial; // Material needed for particle texture.
    public Texture2D[] particleSprites; // Stores various particle sprites.

    private void Start()
    {
        // Get material from renderer component of particle system.
        particleMaterial = GetComponent<ParticleSystemRenderer>().material;
    }

    private void Update()
    {
        // Return if no particle sprites are found.
        if (particleSprites.Length == 0) 
        {
            return;
        }
        
        // Set particle texture using list of particle sprites.
        particleMaterial.mainTexture = particleSprites[JZSpriteCounter.spriteCounter % particleSprites.Length];
    }
}